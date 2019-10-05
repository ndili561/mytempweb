using System.Collections.Generic;
using System.IO;
using System.Linq;
using CRM.Entity.Helper;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Common;
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CRM.Web.Controllers.Common

{
    public class EmailController : BaseController
    {
        private readonly ICommonFacadeApiClient _commonFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        private readonly IPersonApiClient _personApiClient;
        public EmailController(IPersonApiClient personApiClient,ICommonFacadeApiClient commonFacadeApiClient, ILookupApiClient lookupApiClient)
        {
            _personApiClient = personApiClient;
            _commonFacadeApiClient = commonFacadeApiClient;
            _lookupApiClient = lookupApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            PopulateEmailLookup(model.EmailDto);
            return View(model);
        }
        private EmailSearchModel InitializeModel(EmailSearchModel model)
        {
            if (model == null)
            {
                model = new EmailSearchModel
                {
                    SortColumn = "Id",
                    SortDirection = "Desc",
                    PageSize = 8,
                    PageNumber = 1
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.SortColumn))
                {
                    model.SortColumn = "Id";
                    model.SortDirection = "Desc";
                }
            }
            model.TargetGridId = "EmailGrid";
            return model;
        }
        public IActionResult Grid(EmailSearchModel model)
        {
            model = InitializeModel(model);
            var result = _commonFacadeApiClient.GetEmails(model).Result;
            return PartialView(result);
        }

        public IActionResult Inbox()
        {

            var model = InitializeModel(null);
            return View(model);
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDto model)
        {
            if (model.AttachmentFilePaths.Any())
            {
                foreach (var path in model.AttachmentFilePaths)
                {
                    model.Attachments.Add(
                        new DocumentDto
                        {
                            FileContent = System.IO.File.ReadAllBytes(path),
                            Name = Path.GetFileName(path),
                            DocumentTypeName = path.GetDocumentType()
                        }
                        );
                }
            }

            var saveEmail = _commonFacadeApiClient.PostEmail(model).Result;

            foreach (var path in model.AttachmentFilePaths)
            {
                System.IO.File.Delete(path);
            }
            return Json(new { success = true, message = "The email is send successfully." });
        }

        [HttpPost]
        public IActionResult UploadAttachment()
        {
            List<string> uploadedFilePath = new List<string>();
            long size = 0;
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);
                uploadedFilePath.Add(filepath);
                size += file.Length;
                using (var fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            var message = $"{files.Count} file(s) / { size}bytes uploaded successfully!";
            return Json(new { success = true, message, uploadedFilePath });
        }

        public IActionResult ComposeEmail(int personId = 0)
        {
            var model = PopulateEmailLookup(new EmailDto { PersonId = personId });
            if (personId > 0)
            {
                var person = _personApiClient.GetPerson(personId).Result;
                model.Person = person;
            }
           
            var emailCategories = model.EmailCategories.ToList().ConvertAll(x => (BaseLookupDto)x);
            model.EmailCategorySelectListItems = SelectedListHelper.GetSelectListForItems(emailCategories, "");

            var emailLabels = model.EmailLabelTypes.ToList().ConvertAll(x => (BaseLookupDto)x);
            model.EmailLabelTypeSelectListItems = SelectedListHelper.GetSelectListForItems(emailLabels, "", false);
            return View(model);
        }

        public IActionResult EmailView(int id)
        {
            var model = _commonFacadeApiClient.GetEmail(id).Result;
            model = PopulateEmailLookup(model);
            return View(model);
        }

        private EmailDto PopulateEmailLookup(EmailDto email)
        {
            var result = _lookupApiClient.GetLookupUsingOdata(new List<string> { nameof(LookupDto.EmailCategories), nameof(LookupDto.EmailStatuses), nameof(LookupDto.EmailLabelTypes) }).Result;
            var model = result.value.FirstOrDefault();
            if (model != null)
            {
                email.EmailCategories.Clear();
                email.EmailLabelTypes.Clear();
                email.EmailStatuses.Clear();
                email.EmailCategories = model.EmailCategories;
                email.EmailLabelTypes = model.EmailLabelTypes;
                email.EmailStatuses = model.EmailStatuses;
            }

            return email;
        }
    }
}
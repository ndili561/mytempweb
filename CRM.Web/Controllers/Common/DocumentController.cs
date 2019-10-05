using System.IO;
using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;
using CRM.Web.Helpers;
using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Common

{
    public class DocumentController : BaseController
    {
        private readonly ICommonFacadeApiClient _commonFacadeApiClient;
        public DocumentController(ICommonFacadeApiClient commonFacadeApiClient)
        {
            _commonFacadeApiClient = commonFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private DocumentSearchModel InitializeModel(DocumentSearchModel model)
        {
            if (model == null)
            {
                model = new DocumentSearchModel
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
            model.TargetGridId = "DocumentGrid";
            return model;
        }
        public IActionResult Grid(DocumentSearchModel model)
        {
            model = InitializeModel(model);
            var result = _commonFacadeApiClient.GetDocuments(model).Result;
            return PartialView(result);
        }
        public async Task<IActionResult> ViewImage(int id)
        {
            var document = _commonFacadeApiClient.GetDocument(id).Result;
            var file = await _commonFacadeApiClient.GetDocumentFile(new FileMetadata { FileUNCPath = document.RelativePath });
            return PartialView(file);
        }
        public async Task<IActionResult> ViewPdf(int id)
        {
            var document = _commonFacadeApiClient.GetDocument(id).Result;
            if (document.DocumentTypeId == 4)
            {
                var file = await _commonFacadeApiClient.GetDocumentFile(new FileMetadata { FileUNCPath = document.RelativePath });
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", "PDF", document.Name);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    var ms = new MemoryStream(file.FileContent);
                    ms.WriteTo(stream);
                }
                document.RelativePath = "../Documents/PDF/" + document.Name;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(document);
            }
            return View(document);
        }
    }
}
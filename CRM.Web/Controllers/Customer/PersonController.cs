using Core.Utilities.Enum;
using CRM.Entity.Helper;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Customer;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Customer;
using CRM.Entity.Search.Employee;
using CRM.Web.Helpers;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Web.Controllers.Customer

{
    public class PersonController : BaseController
    {
        private readonly ICommonFacadeApiClient _commonApiClient;
        private readonly ICustomerFacadeApiClient _customerApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        private readonly IPropertyFacadeApiClient _propertyFacadeApiClient;
        public PersonController(
            ICommonFacadeApiClient commonApiClient,
            ICustomerFacadeApiClient customerApiClient,
            ILookupApiClient lookupApiClient,
            IPropertyFacadeApiClient propertyFacadeApiClient)
        {
            _commonApiClient = commonApiClient;
            _customerApiClient = customerApiClient;
            _lookupApiClient = lookupApiClient;
            _propertyFacadeApiClient = propertyFacadeApiClient;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private PersonSearchModel InitializeModel(PersonSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonGrid", "Id", "Desc");
            return model;
        }
        [HttpGet]
        public async Task<IActionResult> Grid(PersonSearchModel model)
        {
            model = InitializeModel(model);
            var result = await _customerApiClient.GetPersons(model);
            PopulateValuesFromLookup(result);
            return PartialView(result);
        }

        private void PopulateValuesFromLookup(PersonSearchModel result)
        {
            var lookups = _lookupApiClient.GetLookup().Result;
            foreach (var personDto in result.PersonSearchResult)
            {
                UpdateCustomProperty(personDto, lookups);
            }
        }
        [HttpGet]
        public async Task<IActionResult> HouseholdMember(PersonSearchModel model)
        {
            var person = await _customerApiClient.GetPerson(model.PersonId); 
            model.PersonId = 0;
            model.MainContactPersonId = person.MainContactPersonId??0;
            if (model.MainContactPersonId > 0)
            {
                InitializeSearchModel(model, "HouseholdMemberGrid", "Id", "Desc");
                model = await _customerApiClient.GetPersons(model);
                var householdMembers = model.PersonSearchResult.Where(x => x.Id != person.Id).ToList();
                model.PersonSearchResult.Clear();
                model.PersonSearchResult.AddRange(householdMembers);
                PopulateValuesFromLookup(model);
            }
         
            return PartialView(model);

        }

        private static void UpdateCustomProperty(PersonDto personDto, LookupDto lookups)
        {
            var gender = lookups.Genders.FirstOrDefault(x => x.Id == personDto.GenderId);
            if (gender != null)
                personDto.Gender = gender.Name;
            var ethnicity = lookups.Ethnicities.FirstOrDefault(x => x.Id == personDto.EthnicityId);
            if (ethnicity != null)
                personDto.Ethnicity = ethnicity.Name;
            var language = lookups.Languages.FirstOrDefault(x => x.Id == personDto.PreferredLanguageId);
            if (language != null)
                personDto.Language = language.Name;
            var title = lookups.Titles.FirstOrDefault(x => x.Id == personDto.TitleId);
            if (title != null)
                personDto.Title = title.Name;
            var nationality = lookups.Nationalities.FirstOrDefault(x => x.Id == personDto.NationalityTypeId);
            if (nationality != null)
                personDto.Nationality = nationality.Name;
        }
        [HttpGet]
        public IActionResult Edit(int personId)
        {
            var person = _customerApiClient.GetPerson(personId).Result;
            PopulateDropdownList(person);
            return PartialView(person);
        }
        [HttpGet]
        public IActionResult Create(int? mainContactPersonId)
        {
            PersonDto mainContactPerson = null;
            if (mainContactPersonId.HasValue && mainContactPersonId.Value > 0)
            {
                mainContactPerson = _customerApiClient.GetPerson(mainContactPersonId.Value).Result;
            }
            var person = new PersonDto { MainContactPersonId = mainContactPersonId ,MainContactPerson = mainContactPerson };
            PopulateDropdownList(person);
            return PartialView("Edit", person);
        }

        [AllowAnonymous]
        public JsonResult GetPersonForAutoComplete(string query)
        {
            var personSearchModel = new PersonSearchModel { PageSize = 20, FilterText = query, SortColumn = "Forename" };
            personSearchModel = _customerApiClient.GetPersons(personSearchModel).Result;
            return Json(personSearchModel.PersonSearchResult.Select(p => new
            {
                p.Id,
                p.Forename,
                p.Surname,
                dateofbirth=p.DateOfBirth?.ToShortDateString()??""
            }));
        }

       

        private void PopulateDropdownList(PersonDto person)
        {
            if (person == null)
            {
                return;
            }
            var lookups = _lookupApiClient.GetLookup().Result;
            var titles = lookups.Titles?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var ethinicities = lookups.Ethnicities?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var genders = lookups.Genders?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var languages = lookups.Languages?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var relationships = lookups.Relationships?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var nationalities = lookups.Nationalities?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);

            person.TitleSelectList = SelectedListHelper.GetSelectListForItems(titles, person.TitleId?.ToString());
            person.GenderSelectList = SelectedListHelper.GetSelectListForItems(genders, person.GenderId?.ToString());
            person.EthnicitySelectList = SelectedListHelper.GetSelectListForItems(ethinicities, person.EthnicityId?.ToString());
            person.NationalitySelectList = SelectedListHelper.GetSelectListForItems(nationalities, person.NationalityTypeId?.ToString());
            person.LanguageSelectList = SelectedListHelper.GetSelectListForItems(languages, person.PreferredLanguageId?.ToString());
            person.RelationshipSelectList = SelectedListHelper.GetSelectListForItems(relationships, person.RelationshipId?.ToString());
            GetContactDetails(person, lookups);
        }
        private void GetContactDetails(PersonDto person, LookupDto lookUp)
        {
            var persistedContactDetailIds = person.PersonContactDetails.Select(x => x.ContactByOptionId).ToList();
            person.ContactByOptions = lookUp.ContactByOptions.OrderBy(x => x.SortOrder).ToList();
            foreach (var contactByOption in lookUp.ContactByOptions.Where(x => !persistedContactDetailIds.Contains(x.Id)).OrderBy(x => x.SortOrder).ToList())
            {
                person.PersonContactDetails.Add(new PersonContactDetailDto {PersonId=person.Id, ContactByOptionId = contactByOption.Id, ContactByOption = contactByOption });
            }
            foreach (var contactDetail in person.PersonContactDetails)
            {
                contactDetail.ContactByOption = lookUp.ContactByOptions.FirstOrDefault(x => x.Id == contactDetail.ContactByOptionId);
                if (contactDetail.Id > 0)
                {
                    person.PersonContactDetails.First(x => x.ContactByOptionId == contactDetail.ContactByOptionId).ContactValue = contactDetail.ContactValue;
                    person.PersonContactDetails.First(x => x.ContactByOptionId == contactDetail.ContactByOptionId).Comment = contactDetail.Comment;
                    person.PersonContactDetails.First(x => x.ContactByOptionId == contactDetail.ContactByOptionId).IsSelected = true;
                    person.PersonContactDetails.First(x => x.ContactByOptionId == contactDetail.ContactByOptionId).ContactByOption = lookUp.ContactByOptions.FirstOrDefault(x => x.Id == contactDetail.ContactByOptionId);
                }

            }
        }
        [HttpPost]
        public IActionResult Save(PersonDto model)
        {
            PersonDto entity = null;
            UpdateAuditInformation(model);
            if (model.Id > 0)
            {
                entity = _customerApiClient.GetPerson(model.GlobalIdentityKey.Value.ToString()).Result;
                if (entity == null)
                {
                    ModelState.AddModelError("RowVersion", "The person is deleted by someone else. Please refresh the page and try again.");
                }
                else if (entity.RowVersion.ConvertByteArrayToString() != model.RowVersion.ConvertByteArrayToString())
                {
                    ModelState.AddModelError("RowVersion", "The person is updated by someone else. Please refresh the page and try again.");
                }
                else
                {
                    //Note: Not updating the Person entity only, as the API updated all the associated entity. 
                    entity.Surname = model.Surname;
                    entity.Forename = model.Forename;
                    entity.DateOfBirth = model.DateOfBirth;
                    entity.GenderId = model.GenderId;
                    entity.PreferredLanguageId = model.PreferredLanguageId;
                    entity.NationalityTypeId = model.NationalityTypeId;
                    entity.NationalInsuranceNumber = model.NationalInsuranceNumber;
                    entity.ContactTypeId = model.ContactTypeId;
                    entity.RelationshipId = model.RelationshipId;
                    entity.EthnicityId = model.EthnicityId;
                    entity.TitleId = model.TitleId;
                }
            }
            else
            {
                model.GlobalIdentityKey = Guid.NewGuid();
            }

            if (!ModelState.IsValid)
            {
                GetStateSettings(ViewState.Write);
                PopulateDropdownList(model);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Details", model);
                }
                return PartialView("Edit", model);
            }
            model = model.Id > 0
                ? _customerApiClient.PutPerson(model.Id, model).Result
                : _customerApiClient.PostPerson(model).Result;

            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                PopulateDropdownList(model);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Details", model);
                }
                return PartialView("Edit", model);
            }
            return Json(new { success = string.IsNullOrWhiteSpace(model.ErrorMessage),rowVersion= model.RowVersion, message = model.SuccessMessage });
        }
        [HttpPost]
        public IActionResult SavePersonContactDetails(PersonDto model)
        {

            model = ValidatePersonContactDetails(model);
            var entity = _customerApiClient.GetPerson(model.GlobalIdentityKey.Value.ToString()).Result;

            if (entity == null)
            {
                ModelState.AddModelError("RowVersion", "The person is deleted by someone else. Please refresh the page and try again.");
            }
            else if (entity.RowVersion.ConvertByteArrayToString() != model.RowVersion.ConvertByteArrayToString())
            {
                ModelState.AddModelError("RowVersion", "The person is updated by someone else. Please refresh the page and try again.");
            }
            if (!model.PersonContactDetails.Any())
            {
                ModelState.AddModelError("", "The person contact detail is missing.");
            }
            if (!ModelState.IsValid)
            {
                GetPersonForContactDetails(model);
                return PartialView("_ContactDetail", model);
            }
            var personContactDetails = new List<PersonContactDetailDto>();
            personContactDetails.AddRange(model.PersonContactDetails);
            personContactDetails = personContactDetails.Where(x => x.IsSelected).ToList();

            entity.PersonContactDetails.Clear();
            entity.PersonContactDetails.AddRange(personContactDetails);
            UpdateAuditInformation(entity);
            model = _customerApiClient.UpdatePersonContactDetails(model.Id, entity).Result;


            if (!string.IsNullOrWhiteSpace(model.ErrorMessage))
            {
                GetPersonForContactDetails(model);
                return PartialView("_ContactDetail", model);
            }
            return Json(new { success = string.IsNullOrWhiteSpace(model.ErrorMessage), rowVersion = model.RowVersion, message = model.SuccessMessage });
        }

        private void GetPersonForContactDetails(PersonDto model)
        {
            if (model == null) return;
            PopulateDropdownList(model);
            var lookups = _lookupApiClient.GetLookup().Result;
            UpdateCustomProperty(model, lookups);
        }

        private PersonDto ValidatePersonContactDetails(PersonDto model)
        {
            int ctr = 0;
            foreach (var contactDetail in model.PersonContactDetails)
            {
                if ((contactDetail.IsSelected && string.IsNullOrWhiteSpace(contactDetail.ContactValue)) || (!contactDetail.IsSelected && !string.IsNullOrWhiteSpace(contactDetail.ContactValue)))
                {
                    if (contactDetail.ContactByOptionId != 5)
                    {
                        ModelState.AddModelError($"PersonContactDetails[{ctr}].ContactValue", "The contact value must exist when the checkbox is selected or contact value must be empty when it is not selected.");
                    }
                }
                if (contactDetail.ContactByOptionId == 2)
                {
                    if ((contactDetail.IsSelected && string.IsNullOrWhiteSpace(contactDetail.Comment)) || (!contactDetail.IsSelected && !string.IsNullOrWhiteSpace(contactDetail.Comment)))
                    {
                        ModelState.AddModelError($"PersonContactDetails[{ctr}].Comment", "The name value must exist when the checkbox is selected or name value must be empty when it is not selected.");
                    }
                }
                ctr++;
            }
            return model;
        }
        [HttpGet]
        public IActionResult Details(int personId)
        {
            var person = GetPersonForDetails(personId);
            if (person !=null)
            {
                ViewData["Title"] = person.Title + " " + person.Forename + " " + person.Surname;
                if (string.IsNullOrWhiteSpace(person.TenantCode))
                {
                    person.Tenant = _propertyFacadeApiClient.GetTenantByTenantCode(person.TenantCode).Result;
                    person.Tenant.Property.PropertyDetailView = _propertyFacadeApiClient.GetPropertyDetailView(person.Tenant.Property.PropertyCode).Result;
                }
                if (person.ApplicationId.HasValue)
                {
                    person.VblApplication = _customerApiClient.GetVblApplication(person.ApplicationId.Value).Result;
                }
                if (person.MainContactPersonId.HasValue)
                {
                    var mainPerson = _customerApiClient.GetPerson(person.MainContactPersonId.Value).Result;
                    person.MainContactPerson = mainPerson;
                }
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Details",person);
            }
            return View(person);
        }

        private PersonDto GetPersonForDetails(int personId)
        {
            var person = _customerApiClient.GetPerson(personId).Result;
            GetPersonForContactDetails(person);
            return person;
        }
        [HttpGet]
        public IActionResult ContactAddress(int personId)
        {
            var person = GetPersonForDetails(personId);
            if (!person.PersonAddresses.Any())
            {
                person.PersonAddresses.Add(new PersonAddressDto{PersonId = personId,Address = new AddressDto(),AddressTypeId = 1});
            }
            return PartialView("Address", person.PersonAddresses.ToList());
        }
        public IActionResult ContactDetail(int personId)
        {
            var person = GetPersonForDetails(personId);
            return PartialView("_ContactDetail", person);
        }

        #region ChangeLog
        public IActionResult PersonChangeLog(AuditSearchModel model)
        {
            model = InitializeAuditModel(model);
            model.SelectedTable = "Persons";
            if (Request.IsAjaxRequest())
            {
                var result = _customerApiClient.GetAudits(model).Result;
                return PartialView("ChangeLog", result);
            }
            return PartialView("ChangeLog",model);
        }
        public IActionResult PersonContactByChangeLog(AuditSearchModel model)
        {
            model = InitializeAuditModel(model);
            model.SelectedTable = "PersonContactDetails";
            if (Request.IsAjaxRequest())
            {
               
                var result =  _customerApiClient.GetAudits(model).Result;
                return PartialView("ChangeLog", result);
            }
            return PartialView("ChangeLog", model);
        }
        public IActionResult PersonAddressChangeLog(AuditSearchModel model)
        {
            model = InitializeAuditModel(model);
            model.SelectedTable = "Addresses";
            if (Request.IsAjaxRequest())
            {
                var person = _customerApiClient.GetPerson(model.PersonId).Result;
                var personAddress = person.PersonAddresses?.FirstOrDefault();
                model.AddressId = personAddress?.AddressId??0;
                var result = _customerApiClient.GetAudits(model).Result;
                return PartialView("ChangeLog", result);
            }
            return PartialView("ChangeLog", model);
        }
        private AuditSearchModel InitializeAuditModel(AuditSearchModel model)
        {
            InitializeSearchModel(model, "PersonChangeLogGrid", "Id", "Desc");
        
            return model;
        }
        public async Task<IActionResult> ChangeLogGrid(AuditSearchModel model)
        {
            model = InitializeAuditModel(model);
            var result = await _customerApiClient.GetAudits(model);
            return PartialView(result);
        }
        #endregion

        #region Email
        public IActionResult Email(PersonEmailSearchModel model)
        {
            model = InitializePersonEmailModel(model);
            return PartialView(model);
        }
        private PersonEmailSearchModel InitializePersonEmailModel(PersonEmailSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonEmailGrid", "Id", "Desc");
            return model;
        }
        public async Task<IActionResult> EmailGrid(PersonEmailSearchModel model)
        {
            model = InitializePersonEmailModel(model);
            var result = await _customerApiClient.GetPersonEmails(model);
            return PartialView(result);
        }

        public IActionResult ComposeEmail(int personId)
        {
            var model = new PersonEmailDto { PersonId = personId };
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult SendEmail(int personId)
        {
            var model = new PersonEmailDto { PersonId = personId };
            return PartialView("ComposeEmail", model);
        }
        #endregion

        #region Sms
        public IActionResult Sms(PersonSmsSearchModel model)
        {
            model = InitializePersonSmsModel(model);
            return PartialView(model);
        }
        public IActionResult ComposeSms(int personId)
        {
            var person = _customerApiClient.GetPerson(personId).Result;
            var personContactDetailDto = person?.PersonContactDetails?.FirstOrDefault(x => x.ContactByOptionId == 8);
            var receiverNumber = string.Empty;
            if (personContactDetailDto != null)
            {
                receiverNumber = personContactDetailDto.ContactValue;
            }
            var model = new PersonSmsDto { Person = person, PersonId = personId, Sms = new SmsDto { ReceiverNumber = receiverNumber } };
            return PartialView("_Sms", model);
        }
        private PersonSmsSearchModel InitializePersonSmsModel(PersonSmsSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonSmsGrid", "Id", "Desc");
            return model;
        }
        public async Task<IActionResult> SmsGrid(PersonSmsSearchModel model)
        {
            model = InitializePersonSmsModel(model);
            var result = await _customerApiClient.GetPersonSmses(model);
            return PartialView(result);
        }
        #endregion

        #region Document
        [HttpGet]
        public IActionResult Document(PersonDocumentSearchModel model)
        {
            model = InitializePersonDocumentSearchModel(model);
            var documentTypes = _lookupApiClient.GetLookup().Result.DocumentTypes?.ConvertAll(x => (BaseLookupDto)x);
            model.DocumentTypeSelectList = SelectedListHelper.GetSelectListForItems(documentTypes, "");
            return PartialView(model);
        }
        private PersonDocumentSearchModel InitializePersonDocumentSearchModel(PersonDocumentSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonDocumentGrid", "Id", "Desc");
            return model;
        }
        [HttpGet]
        public async Task<IActionResult> DocumentGrid(PersonDocumentSearchModel model)
        {
            model = InitializePersonDocumentSearchModel(model);
            var result = await _customerApiClient.GetPersonDocuments(model);
            var documentTypes = _lookupApiClient.GetLookup().Result.DocumentTypes?.ConvertAll(x => (BaseLookupDto)x);
            model.DocumentTypeSelectList = SelectedListHelper.GetSelectListForItems(documentTypes, "");
            foreach (var personDocumentDto in result.PersonDocumentSearchResult)
            {
                personDocumentDto.DocumentTypeName = documentTypes?.FirstOrDefault(x => x.Id == personDocumentDto.Document.DocumentTypeId)?.Name;
            }
            var documents = await CreateThumbnail(result.PersonDocumentSearchResult.Where(x => x.Document.DocumentTypeId == 1).ToList());
            return PartialView(result);
        }

        private async Task<IEnumerable<PersonDocumentDto>> CreateThumbnail(IList<PersonDocumentDto> files)
        {
            foreach (var file in files)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", "Images", file.Document.Name);
                var imageFile = await _commonApiClient.GetDocumentFile(new FileMetadata { FileUNCPath = file.Document.RelativePath });
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    var ms = new MemoryStream(imageFile.FileContent);
                    ms.WriteTo(stream);
                }
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var resourceImage = Image.FromStream(stream);
                    var thumb = resourceImage.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                    path = Path.ChangeExtension(path, "thumb");
                    if (!System.IO.File.Exists(path))
                    {
                        thumb.Save(path);
                    }
                    file.ImageThumbnailContent = System.IO.File.ReadAllBytes(path);
                    stream.Dispose();
                }

            }

            return files;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(DocumentDto model)
        {
            if (model.UploadedFile.Length > 0)
            {
                var documentType = model.Name.GetDocumentType();
                var lookups = _lookupApiClient.GetLookup().Result;
                var documentTypes = lookups.DocumentTypes?.ConvertAll(x => (BaseLookupDto)x);
                using (var ms = new MemoryStream())
                {
                    model.UploadedFile.CopyTo(ms);
                    model.FileContent = ms.ToArray();
                    model.DocumentTypeId = documentTypes?.FirstOrDefault(x => x.Name == documentType)?.Id;
                    model.UserId = CurrentUserContactId;
                    model = await _commonApiClient.PostDocument(model);
                    var response = File(ms, model.Name.GetContentType());
                    return Json("Uploaded successfully");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(int documentId)
        {
            if (documentId == 0)
                return Content("documentId cannot be 0.");
            var document = await _commonApiClient.GetDocument(documentId);
            Stream stream;
            if (document.FileContent == null)
            {
                var file = await _commonApiClient.GetDocumentFile(new FileMetadata { FileUNCPath = document.RelativePath });
                stream = new MemoryStream(file.FileContent);
            }
            else
            {
                stream = new MemoryStream(document.FileContent);
            }

            return File(stream, document.Name.GetContentType(), document.Name);
        }
        #endregion

        #region Tenant
        [HttpGet]
        public IActionResult Tenant(TenantSearchModel model)
        {
            model = InitializeTenantModel(model);
            if (Request.IsAjaxRequest())
            {
                var result = _customerApiClient.GetTenants(model).Result;
                result.PropertyCode = result.TenantSearchResult.First()?.Property?.PropertyCode;
                return PartialView(result);
            }
            return View(model);
        }

        private TenantSearchModel InitializeTenantModel(TenantSearchModel model)
        {
            model = InitializeSearchModel(model, "TenantGrid", "TenantCode", "Asc");
            return model;
        }
        [HttpGet]
        public IActionResult TenantGrid(TenantSearchModel model)
        {
            model = InitializeTenantModel(model);
            var result = _customerApiClient.GetTenants(model).Result;
            return PartialView(result);
        }
       
        public IActionResult TenantHistory(string tenantCode)
        {
            var model = new TenantHistoryViewSearchModel { TenantCode = tenantCode, PageSize = int.MaxValue, SortColumn = "TenancyStartDate",SortDirection = "Desc"};
            var result = _propertyFacadeApiClient.GetTenantHistoryViews(model).Result;
            return PartialView(result);
        }
        #endregion

        #region Person Alert
       
        public IActionResult PersonAlert(PersonAlertSearchModel model)
        {
            model = InitializePersonAlertModel(model);
            if (Request.IsAjaxRequest())
            {
                model = _customerApiClient.GetPersonAlerts(model).Result;
                return PartialView("PersonAlert", model);
            }
            return View(model);
        }
        private PersonAlertSearchModel InitializePersonAlertModel(PersonAlertSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonAlertGrid", "RaisedOn", "Desc");
            return model;
        }
       
        public IActionResult PersonAlertGrid(PersonAlertSearchModel model)
        {
            model = InitializePersonAlertModel(model);
            var result = _customerApiClient.GetPersonAlerts(model).Result;
            return PartialView(result);
        }
        #endregion

        #region Person Anti Social Behaviour
       
        public IActionResult PersonAntiSocialBehaviour(PersonAntiSocialBehaviourSearchModel model)
        {
            model = InitializePersonAntiSocialBehaviourModel(model);
            if (Request.IsAjaxRequest())
            {
                model = _customerApiClient.GetPersonAntiSocialBehaviours(model).Result;
                return PartialView("PersonAntiSocialBehaviour", model);
            }
            return View(model);
        }
        private PersonAntiSocialBehaviourSearchModel InitializePersonAntiSocialBehaviourModel(PersonAntiSocialBehaviourSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonAntiSocialBehaviourGrid", "LoggedDate", "Desc");
            return model;
        }
       
        public IActionResult PersonAntiSocialBehaviourGrid(PersonAntiSocialBehaviourSearchModel model)
        {
            model = InitializePersonAntiSocialBehaviourModel(model);
            var result = _customerApiClient.GetPersonAntiSocialBehaviours(model).Result;
            return PartialView(result);
        }
        #endregion

        #region Person Case
       
        public IActionResult PersonCase(PersonCaseSearchModel model)
        {
            model = InitializePersonCaseModel(model);
            if (Request.IsAjaxRequest())
            {
                model = _customerApiClient.GetPersonCases(model).Result;
                return PartialView("PersonCase", model);
            }
            return View(model);
        }
        private PersonCaseSearchModel InitializePersonCaseModel(PersonCaseSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonCaseGrid", "RaisedOn", "Desc");
            return model;
        }
       
        public IActionResult PersonCaseGrid(PersonCaseSearchModel model)
        {
            model = InitializePersonCaseModel(model);
            var result = _customerApiClient.GetPersonCases(model).Result;
            return PartialView(result);
        }
        #endregion

        #region Person Flag
        public IActionResult PersonFlag(PersonFlagSearchModel model)
        {
            model = InitializePersonFlagModel(model);
            if (Request.IsAjaxRequest())
            {
                model = _customerApiClient.GetPersonFlags(model).Result;
                return PartialView("PersonFlag", model);
            }
            return View(model);
        }
        private PersonFlagSearchModel InitializePersonFlagModel(PersonFlagSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonFlagGrid", "RaisedOn", "Desc");
            return model;
        }
       
        public IActionResult PersonFlagGrid(PersonFlagSearchModel model)
        {
            model = InitializePersonFlagModel(model);
            var result = _customerApiClient.GetPersonFlags(model).Result;
            return PartialView(result);
        }
        #endregion

    }
}

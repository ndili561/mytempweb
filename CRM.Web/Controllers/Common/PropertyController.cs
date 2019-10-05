using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity.Helper;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Common;
using CRM.Entity.Search.Customer;
using CRM.Web.Helpers;
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Common

{
    public class PropertyController : BaseController
    {
        private readonly IPropertyFacadeApiClient _propertyFacadeApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        private readonly ICommonFacadeApiClient _commonApiClient;
        private readonly IPersonApiClient _personApiClient;
        public PropertyController(IPropertyFacadeApiClient propertyFacadeApiClient, IPersonApiClient personApiClient,  ICommonFacadeApiClient commonApiClient, ILookupApiClient lookupApiClient)
        {
            _propertyFacadeApiClient = propertyFacadeApiClient;
            _commonApiClient = commonApiClient;
            _personApiClient = personApiClient;
            _lookupApiClient = lookupApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private PropertyDetailViewSearchModel InitializeModel(PropertyDetailViewSearchModel model)
        {
            model = InitializeSearchModel(model, "PropertyGrid", "AssetId", "DESC");
            return model;
        }
        public IActionResult Grid(PropertyDetailViewSearchModel model)
        {
            model = InitializeModel(model);
            var result = _propertyFacadeApiClient.GetPropertyDetailViews(model).Result;
            return PartialView(result);
        }
        public IActionResult Details(string propertyCode,int personId)
        {
            var propertyDetail = _propertyFacadeApiClient.GetPropertyDetails(propertyCode).Result;
            var propertyDetailView = _propertyFacadeApiClient.GetPropertyDetailView(propertyCode).Result;
            propertyDetail.AssetId = propertyDetailView?.AssetId??0;
            propertyDetail.Id = propertyDetailView?.Id??0;
            propertyDetail.PersonId = personId;
            propertyDetail.Person = _personApiClient.GetPerson(personId).Result;
            return View(propertyDetail);
        }
        public IActionResult DetailsFromVoid(string propertyCode)
        {
            var model = _propertyFacadeApiClient.GetPropertyDetailView(propertyCode).Result;
            return PartialView("_DetailsFromVoid", model);
        }
        public IActionResult AssetAttribute(int assetId)
        {
            var model = _propertyFacadeApiClient.GetAsset(assetId).Result;
            return PartialView(model);
        }
        public IActionResult AssetComponent(int assetId)
        {
            var model = _propertyFacadeApiClient.GetAsset(assetId).Result;
            return PartialView(model);
        }
        public IActionResult TrackOperative(string propertyreference, string operativecode)
        {
            var model = new VanLocationSearchModel { EmployeeRef = operativecode };
            var van = _propertyFacadeApiClient.GetVanLocations(model).Result.VanLocationSearchResult.FirstOrDefault();
            var property = _propertyFacadeApiClient.GetPropertyDetailView(propertyreference).Result;
            var route = new DeliveryRouteDto();
            if (van != null)
            {
                route.SourceLongitude = van.Longitude;
                route.SourceLatitude = van.Latitude;
            }
            if (property?.Latitude != null && property.Longitude.HasValue)
            {
                route.DestinationLatitude = (double)property.Latitude.Value;
                route.DestinationLongitude = (double)property.Longitude.Value;
            }
            return View("TrackOperative", route);
        }
        public IActionResult Repair(RepairSearchModel model)
        {
            model = InitializeRepairGridModel(model);
            return PartialView(model);
        }
        private RepairSearchModel InitializeRepairGridModel(RepairSearchModel model)
        {
            model = InitializeSearchModel(model, "RepairGrid", "RepairId", "Desc");
            return model;
        }
        public IActionResult RepairGrid(RepairSearchModel model)
        {
            model = InitializeRepairGridModel(model);
            var result = _propertyFacadeApiClient.GetRepairs(model).Result;
            return PartialView(result);
        }
        

        #region Image
        public IActionResult PropertyImage(PropertyDocumentSearchModel model)
        {
            model = InitializePropertyImageSearchModel(model);
            model.DocumentTypeId = 1;
            model.TargetGridId = "PropertyImageGrid";
            model.SortColumn = "ViewOrder";
            return PartialView(model);
        }
        private PropertyDocumentSearchModel InitializePropertyImageSearchModel(PropertyDocumentSearchModel model)
        {
            model = InitializeSearchModel(model, "ViewOrder", "Id", "Asc");
            return model;
            
        }

        public async Task<IActionResult> PropertyImageGrid(PropertyDocumentSearchModel model)
        {
            model = InitializePropertyImageSearchModel(model);
            var result = await _propertyFacadeApiClient.GetPropertyDocuments(model);
            await CreateThumbnail(result.PropertyDocumentSearchResult.ToList());
            return PartialView(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(DocumentDto model)
        {
            if (model.UploadedFile.Length > 0)
            {
                var saveDocument = new PropertyDocumentDto { PropertyId = model.PropertyId };
                var documentType = model.Name.GetDocumentType();
                var lookups = _lookupApiClient.GetLookup().Result;
                var documentTypes = lookups.DocumentTypes?.ConvertAll(x => (BaseLookupDto)x);
                using (var ms = new MemoryStream())
                {
                    model.UploadedFile.CopyTo(ms);
                    model.FileContent = ms.ToArray();
                    model.DocumentTypeId = documentTypes?.FirstOrDefault(x => x.Name == documentType)?.Id;
                    model.UserId = CurrentUserContactId;
                    model.UploadById = CurrentUserContactId;
                    model.UploadOn = DateTime.Now;
                    saveDocument.Document = model;
                    saveDocument.Comment = model.Comment;
                    saveDocument.IsDefaultImage = model.IsDefaultImage;
                    saveDocument.ViewOrder = model.ViewOrder;
                    saveDocument = await _propertyFacadeApiClient.PostPropertyDocument(saveDocument);
                    var response = File(ms, model.Name.GetContentType());
                    return Json("Uploaded successfully");
                }
            }
            return RedirectToAction("Index");
        }
        private async Task<IEnumerable<PropertyDocumentDto>> CreateThumbnail(IList<PropertyDocumentDto> files)
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

        #endregion

        #region Document
        public IActionResult Document(PropertyDocumentSearchModel model)
        {
             model = InitializePropertyDocumentSearchModel(model);
            var documentTypes = _lookupApiClient.GetLookup().Result.DocumentTypes?.Where(x=>x.Id != 1).ToList().ConvertAll(x => (BaseLookupDto)x);
            model.TargetGridId = "PropertyDocumentGrid";
            model.DocumentTypeSelectList = SelectedListHelper.GetSelectListForItems(documentTypes, "");
            return PartialView(model);
        }
       
        private PropertyDocumentSearchModel InitializePropertyDocumentSearchModel(PropertyDocumentSearchModel model)
        {
            model = InitializeSearchModel(model, "PropertyDocumentGrid", "Id", "Desc");
            return model;
        }
        public async Task<IActionResult> DocumentGrid(PropertyDocumentSearchModel model)
        {
            model = InitializePropertyDocumentSearchModel(model);
            var result = await _propertyFacadeApiClient.GetPropertyDocuments(model);
            var documentTypes = _lookupApiClient.GetLookup().Result.DocumentTypes?.ConvertAll(x => (BaseLookupDto)x);
            model.DocumentTypeSelectList = SelectedListHelper.GetSelectListForItems(documentTypes, "");
            foreach (var personDocumentDto in result.PropertyDocumentSearchResult)
            {
                personDocumentDto.DocumentTypeName = documentTypes?.FirstOrDefault(x => x.Id == personDocumentDto.Document.DocumentTypeId)?.Name;
            }
            return PartialView(result);
        }

      
        [HttpPost]
        public async Task<IActionResult> UploadDocument(DocumentDto model)
        {
            if (model.UploadedFile.Length > 0)
            {
                var saveDocument = new PropertyDocumentDto { PropertyId = model.PropertyId };
                var documentType = model.Name.GetDocumentType();
                var lookups = _lookupApiClient.GetLookup().Result;
                var documentTypes = lookups.DocumentTypes?.ConvertAll(x => (BaseLookupDto)x);
                using (var ms = new MemoryStream())
                {
                    model.UploadedFile.CopyTo(ms);
                    model.FileContent = ms.ToArray();
                    model.DocumentTypeId = documentTypes?.FirstOrDefault(x => x.Name == documentType)?.Id;
                    model.UserId = CurrentUserContactId;
                    model.UploadById = CurrentUserContactId;
                    model.UploadOn = DateTime.Now;
                    saveDocument.Document = model;
                    saveDocument.Comment = model.Comment;
                    saveDocument = await _propertyFacadeApiClient.PostPropertyDocument(saveDocument);
                    var response = File(ms, model.Name.GetContentType());
                    return Json("Uploaded successfully");
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Download(int id)
        {
            if (id == 0)
                return Content("Id cannot be 0.");
            var model = await _propertyFacadeApiClient.GetPropertyDocument(id);
            var document = await _commonApiClient.GetDocument(model.DocumentId);
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

        [HttpGet]
        public JsonResult DeleteDocument(int id)
        {
            if (id > 0)
            {
                var result = _propertyFacadeApiClient.DeletePropertyDocument(id).Result;
                var model = result.IsSuccessStatusCode ? "Deleted successfully." : "API error occured.";
                return Json(new { message = model, id, success = result.IsSuccessStatusCode });
            }
            return Json(new { message = "File deleted successfully.", id, success = true });
        }
     
        #endregion

        public IActionResult Tenant(TenantSearchModel model)
        {
            model = InitializeTenantGridModel(model);
            model = _propertyFacadeApiClient.GetTenants(model).Result;
            return PartialView(model);
        }
        public IActionResult TenantGrid(TenantSearchModel model)
        {
            model = InitializeTenantGridModel(model);
            var result = _propertyFacadeApiClient.GetTenants(model).Result;
            return PartialView(result);
        }
        private TenantSearchModel InitializeTenantGridModel(TenantSearchModel model)
        {
            model = InitializeSearchModel(model, "TenantGrid", "TenantCode", "Desc");
            return model;
        }
        public IActionResult TenantHistory(string tenantCode)
        {
            var model = new TenantHistoryViewSearchModel { TenantCode = tenantCode, PageSize = int.MaxValue, SortColumn = "TenancyStartDate", SortDirection = "Desc" };
            var result = _propertyFacadeApiClient.GetTenantHistoryViews(model).Result;
            return PartialView(result);
        }

        public IActionResult Void(string propertyCode)
        {
            var model = InitializePropertyVoidViewGridModel(null);
            if (!string.IsNullOrWhiteSpace(propertyCode))
            {
                model.PropertyCode = propertyCode;
                model = _propertyFacadeApiClient.GetPropertyVoidViews(model).Result;
            }
            return PartialView(model);
        }
        public IActionResult VoidGrid(PropertyVoidViewSearchModel model)
        {
            model = InitializePropertyVoidViewGridModel(model);
            var result = _propertyFacadeApiClient.GetPropertyVoidViews(model).Result;
            return PartialView(result);
        }
        private PropertyVoidViewSearchModel InitializePropertyVoidViewGridModel(PropertyVoidViewSearchModel model)
        {
            model = InitializeSearchModel(model, "PropertyDetailViewGrid", "VoidId", "Desc");
            return model;
        }

        public async Task<IActionResult> ViewImage(int id)
        {
            var model = _propertyFacadeApiClient.GetPropertyDocument(id).Result;
            var file = await _commonApiClient.GetDocumentFile(new FileMetadata { FileUNCPath = model.Document.RelativePath });
            return PartialView(file);
        }
        public async Task<IActionResult> ViewPdf(int id)
        {
            var model = _propertyFacadeApiClient.GetPropertyDocument(id).Result;
            var file = await _commonApiClient.GetDocumentFile(new FileMetadata { FileUNCPath = model.Document.RelativePath });
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", "PDF", model.Document.Name);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                var ms = new MemoryStream(file.FileContent);
                ms.WriteTo(stream);
            }
            model.Document.RelativePath = "../Documents/PDF/" + model.Document.Name;
            if (Request.IsAjaxRequest())
            {
                return PartialView(model.Document);
            }
            return View(model.Document);
        }
    }
}
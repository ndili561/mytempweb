using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Asset;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Common;
using CRM.Entity.Search.Customer;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Common
{
    /// <summary>
    /// </summary>
    public class PropertyApiClient : BaseClient, IPropertyApiClient
    {
        public PropertyApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PropertyDto> GetProperty(int id)
        {
            var url = CRMApiUri + "/Property/" + id;
            var result = await GetResultFromApi<PropertyDto>(url);
            return result;
        }

        public async Task<PropertyModel> GetPropertyDetails(string propertyCode)
        {
            var url = LogisticsApiUri + "/Property/GetProperty?propertyCode=" + propertyCode;
            var result = await GetResultFromApi<PropertyModel>(url);
            return result;
        }

        public async Task<RepairSearchModel> GetRepairs(RepairSearchModel model)
        {
            var url = LogisticsApiUri + "/Repair/GetRepairs?" + GetFilterStringForRepair(model)+ "&$inlinecount=allpages";
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.RepairSearchResult.Clear();
            model.RepairSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<RepairDto>(item.ToString())));
            return model;
        }

        public async Task<TenantSearchModel> GetTenants(TenantSearchModel model)
        {
            var url = ODataApiUri + "/Tenant?" + GetFilterStringForTenant(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TenantSearchResult.Clear();
            model.TenantSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TenantDto>(item.ToString())));
            return model;
        }
        private string GetFilterStringForTenant(TenantSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.PropertyCode))
                {
                    filterString = ODataFilterConstant.Filter + $"Property/PropertyCode eq '{searchModel.PropertyCode}'";
                }
                filterString += GetFilterStringForAssociatedEntities(new List<string> { "Property", "Person" });
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }


        public async Task<TenantHistoryViewSearchModel> GetTenantHistoryViews(TenantHistoryViewSearchModel model)
        {
            var url = ODataApiUri + "/TenantHistoryView?" + GetFilterStringForTenantHistoryView(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PageSize = searchResultCount;
            model.TenantHistoryViewSearchResult.Clear();
            model.TenantHistoryViewSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TenantHistoryViewDto>(item.ToString())));
            return model;
        }
        private string GetFilterStringForTenantHistoryView(TenantHistoryViewSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.TenantCode))
                {
                    filterString = ODataFilterConstant.Filter + $"TenantCode eq '{searchModel.TenantCode}'";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }

        public async Task<VanLocationSearchModel> GetVanLocations(VanLocationSearchModel model)
        {
            var url = LogisticsApiUri + "/VanLocation/GetVanLocations?" + GetFilterStringForVanLocation(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.VanLocationSearchResult.Clear();
            model.VanLocationSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<VanLocationDto>(item.ToString())));
            return model;
        }


        public async Task<AssetModel> GetAsset(int assetId)
        {
            var url = AssetApiUri + "/Asset/GetAsset?assetId=" + assetId;
            var model = await GetResultFromApi<AssetModel>(url);
            return model;
        }
        public async Task<PropertyDocumentSearchModel> GetPropertyDocuments(PropertyDocumentSearchModel model)
        {
            var url = ODataApiUri + "/PropertyDocument?" + GetFilterStringForDocument(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PropertyDocumentSearchResult.Clear();
            try
            {
                model.PropertyDocumentSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PropertyDocumentDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }
      
     
        private string GetFilterStringForDocument(PropertyDocumentSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.PropertyId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"PropertyId eq {searchModel.PropertyId}";
                    if (!string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString += $" and contains(Comment,'{searchModel.FilterText}') eq true";
                    }
                    filterString += ODataFilterConstant.Expand + $"Document";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
        public async Task<PropertySearchModel> GetProperties(PropertySearchModel model)
        {
            var url = ODataApiUri + "/Property?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PropertySearchResult.Clear();
            model.PropertySearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PropertyDto>(item.ToString())));
            return model;
        }

        private string GetFilterString(PropertySearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"contains(Person/Forename,'{searchModel.FilterText}') eq true";
                    }
                    else
                    {
                        filterString += $" or contains(Person/Forename,'{searchModel.FilterText}') eq true";
                    }
                    filterString += $" or contains(Person/Surname,'{searchModel.FilterText}') eq true";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }

        private string GetFilterStringForRepair(RepairSearchModel model)
        {
            var filterString = string.Empty;
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.VisitStatus))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"VisitStatus eq '{model.VisitStatus}'";
                    }
                    else
                    {
                        filterString += $" and VisitStatus eq '{model.VisitStatus}'";
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.RepairTrade))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"RepairTrade eq '{model.RepairTrade}'";
                    }
                    else
                    {

                        filterString += $" and RepairTrade eq '{model.RepairTrade}'";
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.PropertyReference))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"PropertyReference eq '{model.PropertyReference}'";
                    }
                    else
                    {
                        filterString += $" or PropertyReference eq '{model.PropertyReference}'";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(model.OperativeCode))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"OperativeCode eq '{model.OperativeCode}'";
                    }
                    else
                    {
                        filterString += $" or OperativeCode eq '{model.OperativeCode}'";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(model.FilterText))
                {
                    int id;
                    int.TryParse(model.FilterText, out id);
                    if (id > 0)
                    {
                        if (string.IsNullOrWhiteSpace(filterString))
                        {
                            filterString = ODataFilterConstant.Filter + $"(RepairId eq {id}";
                        }
                        else
                        {
                            filterString += $" and (RepairId eq {id}";
                        }
                        filterString += $" or  RepairLineId eq {id}";
                        filterString += $" or  VisitId eq {id} )";
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(filterString))
                        {
                            filterString = ODataFilterConstant.Filter + $"substringof('{model.FilterText}',OperativeName) eq true";
                        }
                        else
                        {
                            filterString += $" or  substringof('{model.FilterText}',OperativeName) eq true";
                        }
                        filterString += $" or  substringof('{model.FilterText}',RepairDescription) eq true";
                        filterString += $" or  substringof('{model.FilterText}',AccessNotes) eq true";
                    }
                }

                AddPageSizeNumberAndSortingInFilterString(model, ref filterString);
            }
            return filterString;
        }

        private string GetFilterStringForVanLocation(VanLocationSearchModel vanLocationSearchModel)
        {
            var filterString = "";

            if (vanLocationSearchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(vanLocationSearchModel.RegistrationNumber))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter +
                                       $"RegistrationNumber eq '{vanLocationSearchModel.RegistrationNumber}'";
                    }
                    else
                    {
                        filterString += $" and  RegistrationNumber eq '{vanLocationSearchModel.RegistrationNumber}'";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(vanLocationSearchModel.EmployeeRef))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter +
                                       $"EmployeeRef eq '{vanLocationSearchModel.EmployeeRef}'";
                    }
                    else
                    {
                        filterString += $" and  EmployeeRef eq '{vanLocationSearchModel.EmployeeRef}'";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(vanLocationSearchModel.FilterText))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter +
                                       $" substringof('{vanLocationSearchModel.FilterText}',Name) eq true";
                    }
                    else
                    {
                        filterString += $" and  substringof('{vanLocationSearchModel.FilterText}',Name) eq true";
                    }
                    filterString += $" and  substringof('{vanLocationSearchModel.FilterText}',RegistrationNumber) eq true";
                }
                AddPageSizeNumberAndSortingInFilterString(vanLocationSearchModel, ref filterString);
            }
            return filterString;
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Customer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Customer
{
    /// <summary>
    /// </summary>
    public class TenantApiClient : BaseClient, ITenantApiClient
    {
        public TenantApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<TenantDto> GetTenantByTenantCode(string tenantCode)
        {
            var result = await GetTenants(new TenantSearchModel
            {
                TenantCode = tenantCode,
                PageSize = 1,
                PageNumber = 1,
                SortColumn = "Id"
            });
            return result.TenantSearchResult.FirstOrDefault();
        }

        public async Task<TenantDto> GetTenant(int tenantId)
        {
            var url = CRMApiUri + "/Tenant/" + tenantId;
            var result = await GetResultFromApi<TenantDto>(url);
            return result;
        }

        public async Task<TenantDto> PostTenant(TenantDto model)
        {
            var url = CRMApiUri + "/Tenant";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<TenantDto> PutTenant(int id,TenantDto model)
        {
            var url = CRMApiUri + "/Tenant/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<TenantSearchModel> GetTenants(TenantSearchModel model)
        {
            var url = ODataApiUri + "/Tenant?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TenantSearchResult.Clear();
            model.TenantSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TenantDto>(item.ToString())));
            return model;
        }

        private string GetFilterString(TenantSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                filterString = ODataFilterConstant.Expand + $"Person,Property";
                if (!string.IsNullOrWhiteSpace(searchModel.TenantCode))
                {
                    filterString += ODataFilterConstant.Filter + $"TenantCode eq '{searchModel.TenantCode}'";
                }
                else if (!string.IsNullOrWhiteSpace(searchModel.PropertyCode))
                {
                    filterString += ODataFilterConstant.Filter + $"Property/PropertyCode eq '{searchModel.PropertyCode}'";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString += ODataFilterConstant.Filter + $"contains(Person/Forename,'{searchModel.FilterText}') eq true";
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
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;
using CRM.Entity;
using CRM.Entity.Helper;
using CRM.Entity.Settings;
using CRM.WebApiClient.Extension;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient
{
    public abstract class BaseClient
    {
        protected readonly IOptions<HttpClientSettings> HttpClientSettings;
        protected static string AssetApiUri;
        protected static string AllocationApiUri;
        protected static string LogisticsApiUri;
        protected static string CRMApiUri;
        protected static string ODataApiUri;
        protected static string DocumentApiUri;
        protected BaseClient(IOptions<HttpClientSettings> httpClientSettings)
        {
            HttpClientSettings = httpClientSettings;
            AssetApiUri = httpClientSettings.Value.AssetApiUri;
            AllocationApiUri = httpClientSettings.Value.AllocationApiUri;
            CRMApiUri = httpClientSettings.Value.CRMApiUri;
            ODataApiUri = httpClientSettings.Value.ODataApiUri;
            DocumentApiUri = httpClientSettings.Value.DocumentApiUri;
            LogisticsApiUri = httpClientSettings.Value.LogisticsApiUri;
        }

        protected static void LogErrorMessage(Exception ex, string customMessage = "")
        {
            var sb = new StringBuilder();
            sb.AppendLine(customMessage);
            while (ex != null)
            {
                sb.AppendLine(ex.Message);
                ex = ex.InnerException;
            }
        }
        protected static async Task<OdataResult> GetOdataResultFromApi(string requestUri)
        {
            var response = await HttpRequestFactory.Get(requestUri);
            var result = JsonConvert.DeserializeObject<OdataResult>(response.Content.ReadAsStringAsync().Result.StandarizedForOdata());
            return result;
        }

        protected static async Task<List<T>>  GetResultsFromApi<T>(string requestUri)
        {
            var response = await HttpRequestFactory.Get(requestUri);
            return response.ContentAsType<List<T>>();
        }
       
        protected static async Task<T> GetResultFromApi<T>(string requestUri)
        {
            var response = await HttpRequestFactory.Get(requestUri);
            return response.ContentAsType<T>();
        }
        protected static async Task<T> PostRequestToApi<T>(string requestUri,T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var response = await HttpRequestFactory.Post(requestUri, model);
            return response.ContentAsType<T>();
        }
        protected static async Task<T> PutRequestToApi<T>(string requestUri, T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var response = await HttpRequestFactory.Put(requestUri, model);
            return response.ContentAsType<T>();
        }
        protected static async Task<T> PatchRequestToApi<T>(string requestUri, T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var response = await HttpRequestFactory.Patch(requestUri, model);
            return response.ContentAsType<T>();
        }
        protected static async Task<HttpResponseMessage> DeleteRequestToApi(string requestUri)
        {
            return await HttpRequestFactory.Delete(requestUri);
        }
        protected void AddPageSizeNumberAndSortingInFilterString(BaseFilterModel baseFilterModel, ref string filterString)
        {
            baseFilterModel.PageNumber = baseFilterModel.PageNumber > 0 ? baseFilterModel.PageNumber : 1;
            baseFilterModel.PageSize = baseFilterModel.PageSize > 0 ? baseFilterModel.PageSize : 8;
            filterString += ODataFilterConstant.Inlinecount;
            filterString += $"{ODataFilterConstant.Top}{baseFilterModel.PageSize}";
            filterString += $"{ODataFilterConstant.Skip}{(baseFilterModel.PageNumber - 1) * baseFilterModel.PageSize}";
            if (string.IsNullOrWhiteSpace(baseFilterModel.SortColumn))
            {
                return;
            }
            filterString += $"{ODataFilterConstant.Orderby}{baseFilterModel.SortColumn}";
            if (baseFilterModel.SortDirection != null && baseFilterModel.SortDirection.ToLower().Contains("desc"))
            {
                filterString += " desc";
            }
            else
            {
                filterString += " asc";
            }
        }
        protected string GetFilterStringForAssociatedEntities(List<string> entities)
        {
            var filterString = string.Empty;
            foreach (var entity in entities)
            {
                if (string.IsNullOrWhiteSpace(filterString))
                {
                    filterString = ODataFilterConstant.Expand + $"{entity}";
                }
                else
                {
                    filterString += $",{entity}";
                }
            }
            return filterString;
        }
        protected string GetFilterStringForLookup(BaseFilterModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"contains(Name,'{searchModel.FilterText}') eq true";
                    }
                    else
                    {
                        filterString += $" or contains(Name,'{searchModel.FilterText}') eq true";
                    }
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}
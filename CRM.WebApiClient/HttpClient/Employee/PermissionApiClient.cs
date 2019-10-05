using System;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Employee
{
    /// <summary>
    /// </summary>
    public class PermissionApiClient : BaseClient, IPermissionApiClient
    {
        public PermissionApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PermissionDto> GetPermission(int id)
        {
            var url = CRMApiUri + "/Permission/" + id;
            var result = await GetResultFromApi<PermissionDto>(url);
            return result;
        }

        public async Task<PermissionDto> PostPermission(PermissionDto model)
        {
            var url = CRMApiUri + "/Permission";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PermissionDto> PutPermission(int id, PermissionDto model)
        {
            var url = CRMApiUri + "/Permission/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PermissionSearchModel> GetPermissions(PermissionSearchModel model)
        {
            var url = CRMApiUri + "/Permission/GetPermissions?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PermissionSearchResult.Clear();
            try
            {
                model.PermissionSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PermissionDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }



        private string GetFilterString(PermissionSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"contains(Forename,'{searchModel.FilterText}') eq true";
                    }
                    else
                    {
                        filterString += $" or contains(Forename,'{searchModel.FilterText}') eq true";
                    }
                    filterString += $" or contains(Surname,'{searchModel.FilterText}') eq true";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

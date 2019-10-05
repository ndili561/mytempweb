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
    public class RoleApiClient : BaseClient, IRoleApiClient
    {
        public RoleApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<RoleDto> GetRole(int id)
        {
            var url = CRMApiUri + "/Role/" + id;
            var result = await GetResultFromApi<RoleDto>(url);
            return result;
        }

        public async Task<RoleDto> PostRole(RoleDto model)
        {
            var url = CRMApiUri + "/Role";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<RoleDto> PutRole(int id,RoleDto model)
        {
            var url = CRMApiUri + "/Role/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<RoleSearchModel> GetRoles(RoleSearchModel model)
        {
            var url = ODataApiUri + "/Role?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.RoleSearchResult.Clear();
            try
            {
                model.RoleSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<RoleDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(RoleSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.ApplicationId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"ApplicationRoles/any(ap: ap/ApplicationId eq {searchModel.ApplicationId})";
                    if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                    {
                        if (!string.IsNullOrWhiteSpace(filterString))
                        {
                            filterString += $" and (contains(RoleName,'{searchModel.FilterText}') eq true";
                            filterString += $" or contains(RoleDescription,'{searchModel.FilterText}') eq true )";
                        }
                    }
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

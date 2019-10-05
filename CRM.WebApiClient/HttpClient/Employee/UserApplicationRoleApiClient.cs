using System;
using System.Collections.Generic;
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
    public class UserApplicationRoleApiClient : BaseClient, IUserApplicationRoleApiClient
    {
        public UserApplicationRoleApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserApplicationRoleDto> GetUserApplicationRole(int id)
        {
            var url = CRMApiUri + "/UserApplicationRole/" + id;
            var result = await GetResultFromApi<UserApplicationRoleDto>(url);
            return result;
        }

        public async Task<UserApplicationRoleDto> PostUserApplicationRole(UserApplicationRoleDto model)
        {
            var url = CRMApiUri + "/UserApplicationRole";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserApplicationRoleDto> PutUserApplicationRole(int id,UserApplicationRoleDto model)
        {
            var url = CRMApiUri + "/UserApplicationRole/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserApplicationRoleSearchModel> GetUserApplicationRoles(UserApplicationRoleSearchModel model)
        {
            var url = ODataApiUri + "/UserApplicationRole?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserApplicationRoleSearchResult.Clear();
            try
            {
                model.UserApplicationRoleSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserApplicationRoleDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserApplicationRoleSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.ApplicationId > 0 && searchModel.UserId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"UserId eq {searchModel.UserId}";
                    filterString += $" and Role/ApplicationId eq {searchModel.ApplicationId}";
                    if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                    {
                        if (!string.IsNullOrWhiteSpace(filterString))
                        {
                            filterString += $" and (contains(Role/RoleName,'{searchModel.FilterText}') eq true";
                            filterString += $" or contains(Role/RoleDescription,'{searchModel.FilterText}') eq true )";
                        }
                    }
                }
                filterString += GetFilterStringForAssociatedEntities(new List<string>{"Role","User"});
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

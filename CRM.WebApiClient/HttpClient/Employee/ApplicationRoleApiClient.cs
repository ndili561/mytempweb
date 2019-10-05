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
    public class ApplicationRoleApiClient : BaseClient, IApplicationRoleApiClient
    {
        public ApplicationRoleApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<ApplicationRoleDto> GetApplicationRole(int applicationRoleId)
        {
            var url = CRMApiUri + "/ApplicationRole/" + applicationRoleId;
            var result = await GetResultFromApi<ApplicationRoleDto>(url);
            return result;
        }

        public async Task<ApplicationRoleDto> PostApplicationRole(ApplicationRoleDto model)
        {
            var url = CRMApiUri + "/ApplicationRole";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<ApplicationRoleDto> PutApplicationRole(int id,ApplicationRoleDto model)
        {
            var url = CRMApiUri + "/ApplicationRole/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<ApplicationRoleSearchModel> GetApplicationRoles(ApplicationRoleSearchModel model)
        {
            var url = ODataApiUri + "/ApplicationRole?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.ApplicationRoleSearchResult.Clear();
            try
            {
                model.ApplicationRoleSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<ApplicationRoleDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(ApplicationRoleSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.ApplicationId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"ApplicationId eq {searchModel.ApplicationId}";
                }
                if (searchModel.RoleId > 0)
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"RoleId eq {searchModel.RoleId}";
                    }
                    else
                    {
                        filterString += $"and RoleId eq {searchModel.RoleId}";
                    }
                }
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"contains(Role/RoleName,'{searchModel.FilterText}') eq true";
                    }
                    else
                    {
                        filterString += $" or contains(Role/RoleName,'{searchModel.FilterText}') eq true";
                    }
                    filterString += $" or contains(Role/RoleDescription,'{searchModel.FilterText}') eq true";
                }
                filterString += GetFilterStringForAssociatedEntities(new List<string> { "RolePermissions" });
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

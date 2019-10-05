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
    public class UserGroupApiClient : BaseClient, IUserGroupApiClient
    {
        public UserGroupApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserGroupDto> GetUserGroup(int id)
        {
            var url = CRMApiUri + "/UserGroup/" + id;
            var result = await GetResultFromApi<UserGroupDto>(url);
            return result;
        }

        public async Task<UserGroupDto> PostUserGroup(UserGroupDto model)
        {
            var url = CRMApiUri + "/UserGroup";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserGroupDto> PutUserGroup(int id,UserGroupDto model)
        {
            var url = CRMApiUri + "/UserGroup/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserGroupSearchModel> GetUserGroups(UserGroupSearchModel model)
        {
            var url = CRMApiUri + "/UserGroup/GetUserGroups?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserGroupSearchResult.Clear();
            try
            {
                model.UserGroupSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserGroupDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserGroupSearchModel searchModel)
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

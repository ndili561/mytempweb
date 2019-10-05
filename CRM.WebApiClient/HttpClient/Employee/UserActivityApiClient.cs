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
    public class UserActivityApiClient : BaseClient, IUserActivityApiClient
    {
        public UserActivityApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserActivityDto> GetUserActivity(int userActivityId)
        {
            var url = CRMApiUri + "/UserActivity/" + userActivityId;
            var result = await GetResultFromApi<UserActivityDto>(url);
            return result;
        }

        public async Task<UserActivityDto> PostUserActivity(UserActivityDto model)
        {
            var url = CRMApiUri + "/UserActivity";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserActivityDto> PutUserActivity(int id,UserActivityDto model)
        {
            var url = CRMApiUri + "/UserActivity/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserActivitySearchModel> GetUserActivities(UserActivitySearchModel model)
        {
            var url = CRMApiUri + "/UserActivity/GetUserActivities?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserActivitySearchResult.Clear();
            try
            {
                model.UserActivitySearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserActivityDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserActivitySearchModel searchModel)
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

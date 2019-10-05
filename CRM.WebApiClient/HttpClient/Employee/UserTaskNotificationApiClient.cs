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
    public class UserTaskNotificationApiClient : BaseClient, IUserTaskNotificationApiClient
    {
        public UserTaskNotificationApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserTaskNotificationDto> GetUserTaskNotification(int id)
        {
            var url = CRMApiUri + "/UserCalendarTaskNotification/" + id;
            var result = await GetResultFromApi<UserTaskNotificationDto>(url);
            return result;
        }

        public async Task<UserTaskNotificationDto> PostUserTaskNotification(UserTaskNotificationDto model)
        {
            var url = CRMApiUri + "/UserCalendarTaskNotification";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserTaskNotificationDto> PutUserTaskNotification(int id,UserTaskNotificationDto model)
        {
            var url = CRMApiUri + "/UserCalendarTaskNotification/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserTaskNotificationSearchModel> GetUserTaskNotifications(UserTaskNotificationSearchModel model)
        {
            var url = CRMApiUri + "/UserCalendarTaskNotification/GetUserCalendarTaskNotifications?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserTaskNotificationSearchResult.Clear();
            try
            {
                model.UserTaskNotificationSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserTaskNotificationDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserTaskNotificationSearchModel searchModel)
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

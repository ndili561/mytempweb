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
    public class UserTaskReminderApiClient : BaseClient, IUserTaskReminderApiClient
    {
        public UserTaskReminderApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserTaskReminderDto> GetUserTaskReminder(int id)
        {
            var url = CRMApiUri + "/UserCalendarTaskReminder/" + id;
            var result = await GetResultFromApi<UserTaskReminderDto>(url);
            return result;
        }

        public async Task<UserTaskReminderDto> PostUserTaskReminder(UserTaskReminderDto model)
        {
            var url = CRMApiUri + "/UserCalendarTaskReminder";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserTaskReminderDto> PutUserTaskReminder(int id,UserTaskReminderDto model)
        {
            var url = CRMApiUri + "/UserCalendarTaskReminder/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserTaskReminderSearchModel> GetUserTaskReminders(UserTaskReminderSearchModel model)
        {
            var url = CRMApiUri + "/UserCalendarTaskReminder/GetUserCalendarTaskReminders?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserTaskReminderSearchResult.Clear();
            try
            {
                model.UserTaskReminderSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserTaskReminderDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserTaskReminderSearchModel searchModel)
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

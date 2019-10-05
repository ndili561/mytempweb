using System.Linq;
using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Lookup
{
    /// <summary>
    /// </summary>
    public class TaskNotificationTypeApiClient : BaseClient, ITaskNotificationTypeApiClient
    {
        public TaskNotificationTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<TaskNotificationTypeDto> GetTaskNotificationType(int taskNotificationTypeId)
        {
            var url = CRMApiUri + "/TaskNotificationType/" + taskNotificationTypeId;
            var result = await GetResultFromApi<TaskNotificationTypeDto>(url);
            return result;
        }

        public async Task<TaskNotificationTypeDto> PostTaskNotificationType(TaskNotificationTypeDto model)
        {
            var url = CRMApiUri + "/TaskNotificationType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<TaskNotificationTypeDto> PutTaskNotificationType(int id,TaskNotificationTypeDto model)
        {
            var url = CRMApiUri + "/TaskNotificationType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<TaskNotificationTypeSearchModel> GetTaskNotificationTypes(TaskNotificationTypeSearchModel model)
        {
            var url = ODataApiUri + "/TaskNotificationType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TaskNotificationTypeSearchResult.Clear();
            model.TaskNotificationTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TaskNotificationTypeDto>(item.ToString())));
            return model;
        }
    }
}

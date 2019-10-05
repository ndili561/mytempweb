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
    public class TaskReminderTypeApiClient : BaseClient, ITaskReminderTypeApiClient
    {
        public TaskReminderTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<TaskReminderTypeDto> GetTaskReminderType(int taskReminderTypeId)
        {
            var url = CRMApiUri + "/TaskReminderType/" + taskReminderTypeId;
            var result = await GetResultFromApi<TaskReminderTypeDto>(url);
            return result;
        }

        public async Task<TaskReminderTypeDto> PostTaskReminderType(TaskReminderTypeDto model)
        {
            var url = CRMApiUri + "/TaskReminderType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<TaskReminderTypeDto> PutTaskReminderType(int id,TaskReminderTypeDto model)
        {
            var url = CRMApiUri + "/TaskReminderType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<TaskReminderTypeSearchModel> GetTaskReminderTypes(TaskReminderTypeSearchModel model)
        {
            var url = ODataApiUri + "/TaskReminderType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TaskReminderTypeSearchResult.Clear();
            model.TaskReminderTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TaskReminderTypeDto>(item.ToString())));
            return model;
        }

    }
}

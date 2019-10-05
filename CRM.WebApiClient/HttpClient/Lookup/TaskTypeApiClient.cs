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
    public class TaskTypeApiClient : BaseClient, ITaskTypeApiClient
    {
        public TaskTypeApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<TaskTypeDto> GetTaskType(int taskReminderTypeId)
        {
            var url = CRMApiUri + "/TaskType/" + taskReminderTypeId;
            var result = await GetResultFromApi<TaskTypeDto>(url);
            return result;
        }

        public async Task<TaskTypeDto> PostTaskType(TaskTypeDto model)
        {
            var url = CRMApiUri + "/TaskType";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<TaskTypeDto> PutTaskType(int id,TaskTypeDto model)
        {
            var url = CRMApiUri + "/TaskType/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<TaskTypeSearchModel> GetTaskTypes(TaskTypeSearchModel model)
        {
            var url = ODataApiUri + "/TaskType?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TaskTypeSearchResult.Clear();
            model.TaskTypeSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TaskTypeDto>(item.ToString())));
            return model;
        }

    }
}

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
    public class TaskStatusApiClient : BaseClient, ITaskStatusApiClient
    {
        public TaskStatusApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<TaskStatusDto> GetTaskStatus(int taskStatusId)
        {
            var url = CRMApiUri + "/TaskStatus/" + taskStatusId;
            var result = await GetResultFromApi<TaskStatusDto>(url);
            return result;
        }

        public async Task<TaskStatusDto> PostTaskStatus(TaskStatusDto model)
        {
            var url = CRMApiUri + "/TaskStatus";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<TaskStatusDto> PutTaskStatus(int id,TaskStatusDto model)
        {
            var url = CRMApiUri + "/TaskStatus/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<TaskStatusSearchModel> GetTaskStatuses(TaskStatusSearchModel model)
        {
            var url = ODataApiUri + "/TaskStatus?" + GetFilterStringForLookup(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TaskStatusSearchResult.Clear();
            model.TaskStatusSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TaskStatusDto>(item.ToString())));
            return model;
        }
    }
}

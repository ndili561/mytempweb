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
    public class TaskApiClient : BaseClient, ITaskApiClient
    {
        public TaskApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<TaskDto> GetTask(int id)
        {
            var url = CRMApiUri + "/Task/" + id;
            var result = await GetResultFromApi<TaskDto>(url);
            return result;
        }

        public async Task<TaskDto> PostTask(TaskDto model)
        {
            var url = CRMApiUri + "/Task";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<TaskDto> PutTask(int id,TaskDto model)
        {
            var url = CRMApiUri + "/Task/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<TaskSearchModel> GetTasks(TaskSearchModel model)
        {
            var url = ODataApiUri + "/Task?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.TaskSearchResult.Clear();
            try
            {
                model.TaskSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<TaskDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(TaskSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    filterString = ODataFilterConstant.Filter + $"contains(Name,'{searchModel.FilterText}') eq true";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

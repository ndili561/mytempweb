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
    public class ApplicationTaskApiClient : BaseClient, IApplicationTaskApiClient
    {
        public ApplicationTaskApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<ApplicationTaskDto> GetApplicationTask(int applicationRoleId)
        {
            var url = CRMApiUri + "/ApplicationTask/" + applicationRoleId;
            var result = await GetResultFromApi<ApplicationTaskDto>(url);
            return result;
        }

        public async Task<ApplicationTaskDto> PostApplicationTask(ApplicationTaskDto model)
        {
            var url = CRMApiUri + "/ApplicationTask";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<ApplicationTaskDto> PutApplicationTask(int id,ApplicationTaskDto model)
        {
            var url = CRMApiUri + "/ApplicationTask/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<ApplicationTaskSearchModel> GetApplicationTasks(ApplicationTaskSearchModel model)
        {
            var url = ODataApiUri + "/ApplicationTask?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.ApplicationTaskSearchResult.Clear();
            try
            {
                model.ApplicationTaskSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<ApplicationTaskDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(ApplicationTaskSearchModel searchModel)
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

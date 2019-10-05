using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public class UserPersonTaskApiClient : BaseClient, IUserPersonTaskApiClient
    {
        public UserPersonTaskApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserPersonTaskDto> GetUserTask(int id)
        {
            var url = CRMApiUri + "/UserPersonTask/" + id;
            var result = await GetResultFromApi<UserPersonTaskDto>(url);
            return result;
        }

        public async Task<UserDto> GetUserTaskCalendarFile(int employeeId)
        {
            var url = CRMApiUri + "/UserTaskCalendarFile/" + employeeId;
            var result = await GetResultFromApi<UserDto>(url);
            return result;
        }

        public async Task<UserDto> PutUserTaskCalendarFileUpload(int employeeId, UserDto model)
        {
            var url = CRMApiUri + "/UserTaskCalendarFile?id="+ employeeId;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserPersonTaskDto> PostUserTask(UserPersonTaskDto model)
        {
            var url = CRMApiUri + "/UserPersonTask";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserPersonTaskDto> PutUserTask(int id,UserPersonTaskDto model)
        {
            var url = CRMApiUri + "/UserPersonTask/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteUserTask(int id)
        {
            var url = CRMApiUri + "/UserPersonTask/" + id;
            var result = await DeleteRequestToApi(url);
            return result;
        }

        public async Task<UserTaskSearchModel> GetUserTasks(UserTaskSearchModel model)
        {
            var url = ODataApiUri + "/UserPersonTask?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserTaskSearchResult.Clear();
            try
            {
                model.UserTaskSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserPersonTaskDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserTaskSearchModel searchModel)
        {
            var entities = new List<string>{nameof(UserPersonTaskDto.TaskType), nameof(UserPersonTaskDto.Task), nameof(UserPersonTaskDto.User) };
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.UserId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"UserId eq {searchModel.UserId }";
                }
                if (searchModel.PersonId > 0)
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"PersonId eq {searchModel.PersonId }";
                    }
                    else
                    {
                        filterString += $"and PersonId eq {searchModel.PersonId }";
                    }
                }
                if (searchModel.TaskTypeId > 0)
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"TaskTypeId eq {searchModel.TaskTypeId }";
                    }
                    else
                    {
                        filterString += $"and TaskTypeId eq {searchModel.TaskTypeId }";
                    }
                }
                filterString += GetFilterStringForAssociatedEntities(entities);
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

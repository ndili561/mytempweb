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
    public class UserApiClient : BaseClient, IUserApiClient
    {
        public UserApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserDto> GetUser(int id)
        {
            var url = CRMApiUri + "/User/" + id;
            var result = await GetResultFromApi<UserDto>(url);
            return result;
        }

        public async Task<UserDto> PostUser(UserDto model)
        {
            var url = CRMApiUri + "/User";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserDto> PutUser(int id,UserDto model)
        {
            var url = CRMApiUri + "/User/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserDto> PatchUserTasks(int id, UserDto model)
        {
            var url = CRMApiUri + "/User/" + id;
            var result = await PatchRequestToApi(url, model);
            return result;
        }

        public async Task<UserSearchModel> GetUsersWithTasks(UserSearchModel model)
        {
            var url = ODataApiUri + "/User?" + GetFilterStringForGetUsersWithTasks(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserSearchResult.Clear();
            model.UserSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserDto>(item.ToString())));
            return model;
        }

        public async Task<UserSearchModel> GetUsers(UserSearchModel model)
        {
            var url = ODataApiUri + "/User?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserSearchResult.Clear();
            model.UserSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserDto>(item.ToString())));
            return model;
        }

        private string GetFilterStringForGetUsersWithTasks(UserSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.ApplicationId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"Applications/any(ap: ap/ApplicationId eq {searchModel.ApplicationId})";
                }
                else if (searchModel.Id > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"Id eq {searchModel.Id}";

                }
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (!string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString += $" and contains(Name,'{searchModel.FilterText}') eq true";
                    }
                    else
                    {
                        filterString = ODataFilterConstant.Filter + $"contains(Name,'{searchModel.FilterText}') eq true";
                    }
                }
                filterString +=  ODataFilterConstant.Expand + $"{ nameof(UserDto.Tasks)}($expand=Task,TaskType)"; 
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }

        private string GetFilterString(UserSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.ApplicationId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"Applications/any(ap: ap/ApplicationId eq {searchModel.ApplicationId})";
                }
                else if(searchModel.Id > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"Id eq {searchModel.Id}";
                   
                }
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (!string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString += $" and contains(Name,'{searchModel.FilterText}') eq true";
                    }
                    else
                    {
                        filterString = ODataFilterConstant.Filter + $"contains(Name,'{searchModel.FilterText}') eq true";
                    }
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

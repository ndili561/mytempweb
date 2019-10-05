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
    public class UserSmsApiClient : BaseClient, IUserSmsApiClient
    {
        public UserSmsApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserSmsDto> GetUserSms(int id)
        {
            var url = CRMApiUri + "/UserSms/" + id;
            var result = await GetResultFromApi<UserSmsDto>(url);
            return result;
        }

        public async Task<UserSmsDto> PostUserSms(UserSmsDto model)
        {
            var url = CRMApiUri + "/UserSms";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserSmsDto> PutUserSms(int id,UserSmsDto model)
        {
            var url = CRMApiUri + "/UserSms/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserSmsSearchModel> GetUserSmses(UserSmsSearchModel model)
        {
            var url = ODataApiUri + "/UserSms?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserSmsSearchResult.Clear();
            try
            {
                model.UserSmsSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserSmsDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserSmsSearchModel searchModel)
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

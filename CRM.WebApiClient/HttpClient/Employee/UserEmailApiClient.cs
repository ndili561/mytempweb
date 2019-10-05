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
    public class UserEmailApiClient : BaseClient, IUserEmailApiClient
    {
        public UserEmailApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserEmailDto> GetUserEmail(int id)
        {
            var url = CRMApiUri + "/UserEmail/" + id;
            var result = await GetResultFromApi<UserEmailDto>(url);
            return result;
        }

        public async Task<UserEmailDto> PostUserEmail(UserEmailDto model)
        {
            var url = CRMApiUri + "/UserEmail";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserEmailDto> PutUserEmail(int id,UserEmailDto model)
        {
            var url = CRMApiUri + "/UserEmail/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserEmailSearchModel> GetUserEmails(UserEmailSearchModel model)
        {
            var url = ODataApiUri + "/UserEmail?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserEmailSearchResult.Clear();
            try
            {
                model.UserEmailSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserEmailDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserEmailSearchModel searchModel)
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

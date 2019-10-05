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
    public class UserDiaryApiClient : BaseClient, IUserDiaryApiClient
    {
        public UserDiaryApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserDiaryDto> GetUserDiary(int id)
        {
            var url = CRMApiUri + "/UserDiary/" + id;
            var result = await GetResultFromApi<UserDiaryDto>(url);
            return result;
        }

        public async Task<UserDiaryDto> PostUserDiary(UserDiaryDto model)
        {
            var url = CRMApiUri + "/UserDiary";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserDiaryDto> PutUserDiary(int id,UserDiaryDto model)
        {
            var url = CRMApiUri + "/UserDiary/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserDiarySearchModel> GetUserDiarys(UserDiarySearchModel model)
        {
            var url = CRMApiUri + "/UserDiary/GetUserDiarys?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserDiarySearchResult.Clear();
            try
            {
                model.UserDiarySearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserDiaryDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserDiarySearchModel searchModel)
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

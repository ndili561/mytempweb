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
    public class UserMessageApiClient : BaseClient, IUserMessageApiClient
    {
        public UserMessageApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserMessageDto> GetUserMessage(int id)
        {
            var url = CRMApiUri + "/UserMessage/" + id;
            var result = await GetResultFromApi<UserMessageDto>(url);
            return result;
        }

        public async Task<UserMessageDto> PostUserMessage(UserMessageDto model)
        {
            var url = CRMApiUri + "/UserMessage";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserMessageDto> PutUserMessage(int id,UserMessageDto model)
        {
            var url = CRMApiUri + "/UserMessage/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserMessageSearchModel> GetUserMessages(UserMessageSearchModel model)
        {
            var url = CRMApiUri + "/UserMessage/GetUserMessages?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserMessageSearchResult.Clear();
            try
            {
                model.UserMessageSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserMessageDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserMessageSearchModel searchModel)
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

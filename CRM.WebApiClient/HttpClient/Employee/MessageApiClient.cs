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
    public class MessageApiClient : BaseClient, IMessageApiClient
    {
        public MessageApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<MessageDto> GetMessage(int id)
        {
            var url = CRMApiUri + "/Message/" + id;
            var result = await GetResultFromApi<MessageDto>(url);
            return result;
        }

        public async Task<MessageDto> PostMessage(MessageDto model)
        {
            var url = CRMApiUri + "/Message";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<MessageDto> PutMessage(int id,MessageDto model)
        {
            var url = CRMApiUri + "/Message/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<MessageSearchModel> GetMessages(MessageSearchModel model)
        {
            var url = CRMApiUri + "/Message/GetMessages?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.MessageSearchResult.Clear();
            try
            {
                model.MessageSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<MessageDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(MessageSearchModel searchModel)
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

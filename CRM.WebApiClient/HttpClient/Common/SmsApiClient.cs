using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Common
{
    /// <summary>
    /// </summary>
    public class SmsApiClient : BaseClient, ISmsApiClient
    {
        public SmsApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<SmsDto> GetSms(int id)
        {
            var url = CRMApiUri + "/Sms/" + id;
            var result = await GetResultFromApi<SmsDto>(url);
            return result;
        }

        public async Task<SmsDto> PostSms(SmsDto model)
        {
            var url = CRMApiUri + "/Sms";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<SmsDto> PutSms(int id,SmsDto model)
        {
            var url = CRMApiUri + "/Sms/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<SmsSearchModel> GetSmses(SmsSearchModel model)
        {
            var url = CRMApiUri + "/Sms/GetSmss?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.SmsSearchResult.Clear();
            model.SmsSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<SmsDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(SmsSearchModel searchModel)
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

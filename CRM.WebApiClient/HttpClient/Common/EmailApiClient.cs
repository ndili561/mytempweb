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
    public class EmailApiClient : BaseClient, IEmailApiClient
    {
        public EmailApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<EmailDto> GetEmail(int id)
        {
            var url = CRMApiUri + "/Email/" + id;
            var result = await GetResultFromApi<EmailDto>(url);
            return result;
        }

        public async Task<EmailDto> PostEmail(EmailDto model)
        {
            var url = CRMApiUri + "/Email";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<EmailDto> PutEmail(int id,EmailDto model)
        {
            var url = CRMApiUri + "/Email/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<EmailSearchModel> GetEmails(EmailSearchModel model)
        {
            var url = CRMApiUri + "/Email/GetEmails?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.EmailSearchResult.Clear();
            model.EmailSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<EmailDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(EmailSearchModel searchModel)
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

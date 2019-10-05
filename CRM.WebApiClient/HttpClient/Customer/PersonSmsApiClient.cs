using System;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Customer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Customer
{
    /// <summary>
    /// </summary>
    public class PersonSmsApiClient : BaseClient, IPersonSmsApiClient
    {
        public PersonSmsApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonSmsDto> GetPersonSms(int id)
        {
            var url = CRMApiUri + "/PersonSms/" + id;
            var result = await GetResultFromApi<PersonSmsDto>(url);
            return result;
        }

        public async Task<PersonSmsDto> PostPersonSms(PersonSmsDto model)
        {
            var url = CRMApiUri + "/PersonSms";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonSmsDto> PutPersonSms(int id,PersonSmsDto model)
        {
            var url = CRMApiUri + "/PersonSms/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonSmsSearchModel> GetPersonSmses(PersonSmsSearchModel model)
        {
            var url = ODataApiUri + "/PersonSms?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonSmsSearchResult.Clear();
            try
            {
                model.PersonSmsSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonSmsDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(PersonSmsSearchModel searchModel)
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

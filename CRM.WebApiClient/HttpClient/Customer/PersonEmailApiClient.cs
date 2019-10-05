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
    public class PersonEmailApiClient : BaseClient, IPersonEmailApiClient
    {
        public PersonEmailApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonEmailDto> GetPersonEmail(int id)
        {
            var url = CRMApiUri + "/PersonEmail/" + id;
            var result = await GetResultFromApi<PersonEmailDto>(url);
            return result;
        }

        public async Task<PersonEmailDto> PostPersonEmail(PersonEmailDto model)
        {
            var url = CRMApiUri + "/PersonEmail";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonEmailDto> PutPersonEmail(int id,PersonEmailDto model)
        {
            var url = CRMApiUri + "/PersonEmail/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonEmailSearchModel> GetPersonEmails(PersonEmailSearchModel model)
        {
            var url = ODataApiUri + "/PersonEmail?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonEmailSearchResult.Clear();
            try
            {
                model.PersonEmailSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonEmailDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(PersonEmailSearchModel searchModel)
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

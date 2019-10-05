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
    public class PersonAddressApiClient : BaseClient, IPersonAddressApiClient
    {
        public PersonAddressApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonAddressDto> GetPersonAddress(int personAddressId)
        {
            var url = CRMApiUri + "/People/" + personAddressId;
            var result = await GetResultFromApi<PersonAddressDto>(url);
            return result;
        }

        public async Task<PersonAddressDto> PostPersonAddress(PersonAddressDto model)
        {
            var url = CRMApiUri + "/PersonAddress";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonAddressDto> PutPersonAddress(int id,PersonAddressDto model)
        {
            var url = CRMApiUri + "/PersonAddress/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonAddressSearchModel> GetPersonAddresses(PersonAddressSearchModel model)
        {
            var url = CRMApiUri + "/PersonAddress/GetPersonAddresss?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonAddressSearchResult.Clear();
            try
            {
                model.PersonAddressSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonAddressDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(PersonAddressSearchModel searchModel)
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

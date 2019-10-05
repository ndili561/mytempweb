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
    public class AddressApiClient : BaseClient, IAddressApiClient
    {
        public AddressApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AddressDto> GetAddress(int addressId)
        {
            var url = CRMApiUri + "/Address/" + addressId;
            var result = await GetResultFromApi<AddressDto>(url);
            return result;
        }

        public async Task<AddressDto> PostAddress(AddressDto model)
        {
            var url = CRMApiUri + "/Address";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<AddressDto> PutAddress(int id,AddressDto model)
        {
            var url = CRMApiUri + "/Address/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<AddressSearchModel> GetAddresses(AddressSearchModel model)
        {
            var url = CRMApiUri + "/Address/GetAddresss?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AddressSearchResult.Clear();
            try
            {
                model.AddressSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AddressDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(AddressSearchModel searchModel)
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

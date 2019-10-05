using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Helper;
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
    public class PersonApiClient : BaseClient, IPersonApiClient
    {
        public PersonApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonDto> GetPerson(string globalIdentityKey)
        {
            var url = ODataApiUri + $"/Person?$filter=GlobalIdentityKey eq { globalIdentityKey}&$top=1&$expand=PersonAddresses($expand=Address),Applications,PersonContactDetails";
            var result = await GetOdataResultFromApi(url);
            return result.Items.Select(item => JsonConvert.DeserializeObject<PersonDto>(item.ToString())).FirstOrDefault();
        }

        public async Task<PersonDto> GetPerson(int personId)
        {
            var url = CRMApiUri + "/Person/" + personId;
            var result = await GetResultFromApi<PersonDto>(url);
            return result;
        }

        public async Task<List<PersonDto>> GetPersonByVblApplicationId(int vblApplicationId)
        {
            var model = await GetPersons(new PersonSearchModel{ApplicationId = vblApplicationId ,SortColumn = "Id",PageSize = int.MaxValue});
            return model.PersonSearchResult.ToList();
        }

        public async Task<List<PersonDto>> GetPersonByMainContactId(int mainContactPersonId)
        {
            var model = await GetPersons(new PersonSearchModel { MainContactPersonId= mainContactPersonId, SortColumn = "Id", PageSize = int.MaxValue });
            return model.PersonSearchResult.ToList();
        }

        public async Task<PersonDto> PostPerson(PersonDto model)
        {
            var url = CRMApiUri + "/Person";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonDto> PutPerson(int id, PersonDto model)
        {
            var url = CRMApiUri + "/Person/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonDto> UpdatePersonContactDetails(int id, PersonDto model)
        {
            var url = CRMApiUri + "/Person/UpdatePersonContactDetails?id=" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonSearchModel> GetPersons(PersonSearchModel model)
        {
            var url = ODataApiUri + "/Person?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonSearchResult.Clear();
            model.PersonSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonDto>(item.ToString())));
            return model;
        }



        private string GetFilterString(PersonSearchModel searchModel)
        {
            var filterString = ODataFilterConstant.Filter + "Id gt 0";
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    int id;
                    if (int.TryParse(searchModel.FilterText, out id))
                    {
                        filterString += $" and ApplicationId eq {id}";
                    }
                    else if(string.IsNullOrWhiteSpace(searchModel.Forename) && string.IsNullOrWhiteSpace(searchModel.Surname))
                    {
                        filterString += $" and (startswith(Forename,'{searchModel.FilterText}') eq true or startswith(Surname,'{searchModel.FilterText}') eq true)";
                    }
                }
                if (!string.IsNullOrWhiteSpace(searchModel.Forename))
                {
                    filterString += $" and contains(Forename,'{searchModel.Forename}') eq true";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.Surname))
                {
                    filterString += $" and contains(Surname,'{searchModel.Surname}') eq true";
                }
                
                if (searchModel.MainContactPersonId > 0)
                {
                    filterString += $" and MainContactPersonId eq {searchModel.MainContactPersonId}";
                }
                if (searchModel.DateOfBirth.HasValue)
                {
                    filterString += $" and DateOfBirth eq datetime'{searchModel.DateOfBirth.Value.Date.ToString("yyyy-MM-dd")}'";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.NationalInsuranceNumber))
                {
                    filterString += $" and NationalInsuranceNumber eq '{searchModel.NationalInsuranceNumber}')";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.Address))
                {
                    filterString += $" and PersonAddresses/any(pa: contains(pa/Address/AddressLine1,'{searchModel.Address}') eq true)";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.Postcode))
                {
                    filterString += $" and PersonAddresses/any(pa: contains(pa/Address/Postcode ,'{searchModel.Postcode}' )eq true))";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.Email))
                {
                    filterString += $" and PersonContactDetails/any(pcd:pcd/ContactByOption/Id eq 4 and pcd/ContactValue eq '{searchModel.Email}')";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.MobileNumber))
                {
                    filterString += $" and PersonContactDetails/any(pcd:pcd/ContactByOption/Id eq 3 and pcd/ContactValue eq '{searchModel.MobileNumber?.RemoveSpaces()}')";
                }
                if (!string.IsNullOrWhiteSpace(searchModel.TelephoneNumber))
                {
                    filterString += $" and PersonContactDetails/any(pcd:pcd/ContactByOption/Id eq 1 and pcd/ContactValue eq '{searchModel.TelephoneNumber?.RemoveSpaces()}')";
                }
               
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

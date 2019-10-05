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
    public class PersonFlagApiClient : BaseClient, IPersonFlagApiClient
    {
        public PersonFlagApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }
        public async Task<PersonDto> GetPerson(int personId)
        {
            var url = CRMApiUri + "/Person/" + personId;
            var result = await GetResultFromApi<PersonDto>(url);
            return result;
        }
        public async Task<PersonFlagDto> GetPersonFlag(int personFlagId)
        {
            var url = CRMApiUri + "/PersonFlag/" + personFlagId;
            var result = await GetResultFromApi<PersonFlagDto>(url);
            return result;
        }

        public async Task<PersonFlagDto> PostPersonFlag(PersonFlagDto model)
        {
            var url = CRMApiUri + "/PersonFlag";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonFlagDto> PutPersonFlag(int id,PersonFlagDto model)
        {
            var url = CRMApiUri + "/PersonFlag/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonFlagSearchModel> GetPersonFlags(PersonFlagSearchModel model)
        {
            var url = ODataApiUri + "/PersonFlag?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonFlagSearchResult.Clear();
            model.PersonFlagSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonFlagDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(PersonFlagSearchModel searchModel)
        {
            var filterString = ODataFilterConstant.Expand + $"Person,FlagType,FlagGroup,PersonFlagComments";
            if (searchModel.PersonId > 0)
            {
                filterString += ODataFilterConstant.Filter + $"PersonId eq {searchModel.PersonId}";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
            {
                if (string.IsNullOrWhiteSpace(filterString))
                {
                    filterString += ODataFilterConstant.Filter + $"contains(Person/Forename,'{searchModel.FilterText}') eq true";
                }
                else
                {
                    filterString += $" or contains(Person/Forename,'{searchModel.FilterText}') eq true";
                }
                filterString += $" or contains(Person/Surname,'{searchModel.FilterText}') eq true";
                filterString += $" or contains(Details,'{searchModel.FilterText}') eq true";
            }
            AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            return filterString;
        }
    }
}

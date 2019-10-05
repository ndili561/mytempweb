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
    public class PersonCaseApiClient : BaseClient, IPersonCaseApiClient
    {
        public PersonCaseApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }
      
        public async Task<PersonCaseDto> GetPersonCase(int personCaseId)
        {
            var url = CRMApiUri + "/PersonCase/" + personCaseId;
            var result = await GetResultFromApi<PersonCaseDto>(url);
            return result;
        }

        public async Task<PersonCaseDto> PostPersonCase(PersonCaseDto model)
        {
            var url = CRMApiUri + "/PersonCase";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonCaseDto> PutPersonCase(int id,PersonCaseDto model)
        {
            var url = CRMApiUri + "/PersonCase/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonCaseSearchModel> GetPersonCases(PersonCaseSearchModel model)
        {
            var url = ODataApiUri + "/PersonCase?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonCaseSearchResult.Clear();
            try
            {
                model.PersonCaseSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonCaseDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(PersonCaseSearchModel searchModel)
        {
            var filterString = ODataFilterConstant.Expand + $"Person";
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

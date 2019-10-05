using System.Linq;
using System.Net.Http;
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
    public class PersonAntiSocialBehaviourApiClient : BaseClient, IPersonAntiSocialBehaviourApiClient
    {
        public PersonAntiSocialBehaviourApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonAntiSocialBehaviourDto> GetPersonAntiSocialBehaviour(int personAlertId)
        {
            var url = CRMApiUri + "/PersonAntiSocialBehaviour/" + personAlertId;
            var result = await GetResultFromApi<PersonAntiSocialBehaviourDto>(url);
            return result;
        }

        public async Task<PersonAntiSocialBehaviourDto> PostPersonAntiSocialBehaviour(PersonAntiSocialBehaviourDto model)
        {
            var url = CRMApiUri + "/PersonAntiSocialBehaviour";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonAntiSocialBehaviourDto> PutPersonAntiSocialBehaviour(int id,PersonAntiSocialBehaviourDto model)
        {
            var url = CRMApiUri + "/PersonAntiSocialBehaviour/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<HttpResponseMessage> DeletePersonAntiSocialBehaviour(int id)
        {
            var url = CRMApiUri + "/PersonAntiSocialBehaviour/" + id;
            var result = await DeleteRequestToApi(url);
            return result;
        }

        public async Task<PersonAntiSocialBehaviourSearchModel> GetPersonAntiSocialBehaviours(PersonAntiSocialBehaviourSearchModel model)
        {
            var url = ODataApiUri + "/PersonAntiSocialBehaviour?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonAntiSocialBehaviourSearchResult.Clear();
            model.PersonAntiSocialBehaviourSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonAntiSocialBehaviourDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(PersonAntiSocialBehaviourSearchModel searchModel)
        {
            var filterString = ODataFilterConstant.Expand + $"Person,PersonAntiSocialBehaviourCaseNotes";
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
            }
            AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            return filterString;
        }
    }
}

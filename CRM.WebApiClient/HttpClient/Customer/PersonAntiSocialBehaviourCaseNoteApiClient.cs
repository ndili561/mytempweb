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
    public class PersonAntiSocialBehaviourCaseNoteApiClient : BaseClient, IPersonAntiSocialBehaviourCaseNoteApiClient
    {
        public PersonAntiSocialBehaviourCaseNoteApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonAntiSocialBehaviourCaseNoteDto> GetPersonAntiSocialBehaviourCaseNote(int personAlertId)
        {
            var url = CRMApiUri + "/PersonAntiSocialBehaviourCaseNote/" + personAlertId;
            var result = await GetResultFromApi<PersonAntiSocialBehaviourCaseNoteDto>(url);
            return result;
        }

        public async Task<PersonAntiSocialBehaviourCaseNoteDto> PostPersonAntiSocialBehaviourCaseNote(PersonAntiSocialBehaviourCaseNoteDto model)
        {
            var url = CRMApiUri + "/PersonAntiSocialBehaviourCaseNote";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonAntiSocialBehaviourCaseNoteDto> PutPersonAntiSocialBehaviourCaseNote(int id,PersonAntiSocialBehaviourCaseNoteDto model)
        {
            var url = CRMApiUri + "/PersonAntiSocialBehaviourCaseNote/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<HttpResponseMessage> DeletePersonAntiSocialBehaviourCaseNote(int id)
        {
            var url = CRMApiUri + "/PersonAntiSocialBehaviourCaseNote/" + id;
            var result = await DeleteRequestToApi(url);
            return result;
        }

        public async Task<PersonAntiSocialBehaviourCaseNoteSearchModel> GetPersonAntiSocialBehaviourCaseNotes(PersonAntiSocialBehaviourCaseNoteSearchModel model)
        {
            var url = ODataApiUri + "/PersonAntiSocialBehaviourCaseNote?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonAntiSocialBehaviourCaseNoteSearchResult.Clear();
            model.PersonAntiSocialBehaviourCaseNoteSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonAntiSocialBehaviourCaseNoteDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(PersonAntiSocialBehaviourCaseNoteSearchModel searchModel)
        {
            var filterString = ODataFilterConstant.Expand + $"Person,PersonAntiSocialBehaviourCaseNoteCaseNotes";
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

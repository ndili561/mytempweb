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
    public class PersonFlagCommentApiClient : BaseClient, IPersonFlagCommentApiClient
    {
        public PersonFlagCommentApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonFlagCommentDto> GetPersonFlagComment(int personFlagCommentId)
        {
            var url = CRMApiUri + "/PersonFlagComment/" + personFlagCommentId;
            var result = await GetResultFromApi<PersonFlagCommentDto>(url);
            return result;
        }
     
        public async Task<PersonFlagCommentDto> PostPersonFlagComment(PersonFlagCommentDto model)
        {
            var url = CRMApiUri + "/PersonFlagComment";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonFlagCommentDto> PutPersonFlagComment(int id,PersonFlagCommentDto model)
        {
            var url = CRMApiUri + "/PersonFlagComment/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonFlagCommentSearchModel> GetPersonFlagComments(PersonFlagCommentSearchModel model)
        {
            var url = ODataApiUri + "/PersonFlagComment?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonFlagCommentSearchResult.Clear();
            model.PersonFlagCommentSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonFlagCommentDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(PersonFlagCommentSearchModel searchModel)
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
            }
            AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            return filterString;
        }
    }
}

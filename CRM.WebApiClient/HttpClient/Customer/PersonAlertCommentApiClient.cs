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
    public class PersonAlertCommentApiClient : BaseClient, IPersonAlertCommentApiClient
    {
        public PersonAlertCommentApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonAlertCommentDto> GetPersonAlertComment(int personAlertCommentId)
        {
            var url = CRMApiUri + "/PersonAlertComment/" + personAlertCommentId;
            var result = await GetResultFromApi<PersonAlertCommentDto>(url);
            return result;
        }
     
        public async Task<PersonAlertCommentDto> PostPersonAlertComment(PersonAlertCommentDto model)
        {
            var url = CRMApiUri + "/PersonAlertComment";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonAlertCommentDto> PutPersonAlertComment(int id,PersonAlertCommentDto model)
        {
            var url = CRMApiUri + "/PersonAlertComment/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonAlertCommentSearchModel> GetPersonAlertComments(PersonAlertCommentSearchModel model)
        {
            var url = ODataApiUri + "/PersonAlertComment?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonAlertCommentSearchResult.Clear();
            model.PersonAlertCommentSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonAlertCommentDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(PersonAlertCommentSearchModel searchModel)
        {
            var filterString = ODataFilterConstant.Expand + $"PersonAlert";
            if (searchModel.PersonAlertId > 0)
            {
                filterString += ODataFilterConstant.Filter + $"PersonAlertId eq {searchModel.PersonAlertId}";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
            {
                if (string.IsNullOrWhiteSpace(filterString))
                {
                    filterString += ODataFilterConstant.Filter + $"contains(Comment,'{searchModel.FilterText}') eq true";
                }
                else
                {
                    filterString += $" or contains(Comment,'{searchModel.FilterText}') eq true";
                }
                filterString += $" or contains(Comment,'{searchModel.FilterText}') eq true";
            }
            AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            return filterString;
        }
    }
}

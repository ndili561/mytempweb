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
    public class PersonAlertApiClient : BaseClient, IPersonAlertApiClient
    {
        public PersonAlertApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<PersonAlertDto> GetPersonAlert(int personAlertId)
        {
            var url = CRMApiUri + "/PersonAlert/" + personAlertId;
            var result = await GetResultFromApi<PersonAlertDto>(url);
            return result;
        }
        public async Task<PersonDto> GetPerson(int personId)
        {
            var url = CRMApiUri + "/Person/" + personId;
            var result = await GetResultFromApi<PersonDto>(url);
            return result;
        }

        public async Task<PersonAlertDto> PostPersonAlert(PersonAlertDto model)
        {
            var url = CRMApiUri + "/PersonAlert";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<PersonAlertDto> PutPersonAlert(int id,PersonAlertDto model)
        {
            var url = CRMApiUri + "/PersonAlert/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<PersonAlertSearchModel> GetPersonAlerts(PersonAlertSearchModel model)
        {
            var url = ODataApiUri + "/PersonAlert?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.PersonAlertSearchResult.Clear();
            model.PersonAlertSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<PersonAlertDto>(item.ToString())));
            return model;
        }

      

        private string GetFilterString(PersonAlertSearchModel searchModel)
        {
            var filterString = ODataFilterConstant.Expand + $"Person,AlertType,AlertStatus,AlertGroup,PersonAlertComments";
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

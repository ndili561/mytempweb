using System;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Employee
{
    /// <summary>
    /// </summary>
    public class UserDocumentApiClient : BaseClient, IUserDocumentApiClient
    {
        public UserDocumentApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<UserDocumentDto> GetUserDocument(int id)
        {
            var url = CRMApiUri + "/UserDocument/" + id;
            var result = await GetResultFromApi<UserDocumentDto>(url);
            return result;
        }

        public async Task<UserDocumentDto> PostUserDocument(UserDocumentDto model)
        {
            var url = CRMApiUri + "/UserDocument";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<UserDocumentDto> PutUserDocument(int id,UserDocumentDto model)
        {
            var url = CRMApiUri + "/UserDocument/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<UserDocumentSearchModel> GetUserDocuments(UserDocumentSearchModel model)
        {
            var url = ODataApiUri + "/UserDocument?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.UserDocumentSearchResult.Clear();
            try
            {
                model.UserDocumentSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<UserDocumentDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(UserDocumentSearchModel searchModel)
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

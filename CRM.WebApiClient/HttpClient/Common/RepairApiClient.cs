using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Common
{
    /// <summary>
    /// </summary>
    public class RepairApiClient : BaseClient, IRepairApiClient
    {
        public RepairApiClient(IOptions<HttpClientSettings> httpClientSettings):base(httpClientSettings)
        {
        }
      
        public async Task<RepairSearchModel> GetRepairs(RepairSearchModel model)
        {
            var url = LogisticsApiUri + "/Repair/GetRepairs?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.RepairSearchResult.Clear();
            model.RepairSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<RepairDto>(item.ToString())));
            return model;
        }

        private string GetFilterString(RepairSearchModel model)
        {
            var filterString = string.Empty;
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.VisitStatus))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"VisitStatus eq '{model.VisitStatus}'";
                    }
                    else
                    {
                        filterString += $" and VisitStatus eq '{model.VisitStatus}'";
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.RepairTrade))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"RepairTrade eq '{model.RepairTrade}'";
                    }
                    else
                    {

                        filterString += $" and RepairTrade eq '{model.RepairTrade}'";
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.PropertyReference))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"PropertyReference eq '{model.PropertyReference}'";
                    }
                    else
                    {
                        filterString += $" or PropertyReference eq '{model.PropertyReference}'";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(model.OperativeCode))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"OperativeCode eq '{model.OperativeCode}'";
                    }
                    else
                    {
                        filterString += $" or OperativeCode eq '{model.OperativeCode}'";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(model.FilterText))
                {
                    int id;
                    int.TryParse(model.FilterText, out id);
                    if (id > 0)
                    {
                        if (string.IsNullOrWhiteSpace(filterString))
                        {
                            filterString = ODataFilterConstant.Filter + $"(RepairId eq {id}";
                        }
                        else
                        {
                            filterString += $" and (RepairId eq {id}";
                        }
                        filterString += $" or  RepairLineId eq {id}";
                        filterString += $" or  VisitId eq {id} )";
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(filterString))
                        {
                            filterString = ODataFilterConstant.Filter + $"substringof('{model.FilterText}',OperativeName) eq true";
                        }
                        else
                        {
                            filterString += $" or  substringof('{model.FilterText}',OperativeName) eq true";
                        }
                        filterString += $" or  substringof('{model.FilterText}',RepairDescription) eq true";
                        filterString += $" or  substringof('{model.FilterText}',AccessNotes) eq true";
                    }
                }

                AddPageSizeNumberAndSortingInFilterString(model, ref filterString);
            }
            return filterString;
        }
    }
}

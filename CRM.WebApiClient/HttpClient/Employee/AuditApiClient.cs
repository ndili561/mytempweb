using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using CRM.Entity;
using CRM.Entity.Search.Employee;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient
{
    /// <summary>
    /// </summary>
    public class AuditApiClient : BaseClient, IAuditApiClient
    {
        public AuditApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<AuditSearchModel> GetAudits(AuditSearchModel model)
        {
            var url = ODataApiUri + "/Audit?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.AuditSearchResult.Clear();
            try
            {
                model.AuditSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<AuditModel>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

        private string GetFilterString(AuditSearchModel auditSearchModel)
        {
            var filterString = string.Empty;
            if (auditSearchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(auditSearchModel.SelectedTable))
                {
                   
                    if (auditSearchModel.PersonId > 0)
                    {
                        if (auditSearchModel.SelectedTable == "Persons")
                        {
                            filterString = ODataFilterConstant.Filter + @"KeyValues eq '{""Id"":";
                            filterString += auditSearchModel.PersonId + "}'";
                        }
                        else if (auditSearchModel.SelectedTable == "Addresses")
                        {
                            filterString = ODataFilterConstant.Filter + @"KeyValues eq '{""Id"":";
                            filterString += auditSearchModel.AddressId + "}'";
                        }
                        else
                        {
                            filterString = ODataFilterConstant.Filter + @"(contains(NewValues,'""PersonId"":";
                            filterString += auditSearchModel.PersonId + "') eq true";
                            filterString += @" or contains(OldValues,'""PersonId"":";
                            filterString += auditSearchModel.PersonId + "') eq true)";
                        }
                    }
                    else if (auditSearchModel.Id > 0)
                    {
                        filterString+=$"Id eq {auditSearchModel.Id}";
                    }
                    if (!string.IsNullOrWhiteSpace(auditSearchModel.SelectedTable))
                    {
                        filterString += $" and TableName eq '{auditSearchModel.SelectedTable}'";
                    }
                }
               
                AddPageSizeNumberAndSortingInFilterString(auditSearchModel, ref filterString);
            }
            return filterString;
        }
    }
}

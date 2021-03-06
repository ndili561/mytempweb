﻿using System;
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
    public class WebApplicationApiClient : BaseClient, IWebApplicationApiClient
    {
        public WebApplicationApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<WebApplicationDto> GetWebApplication(int id)
        {
            var url = CRMApiUri + "/WebApplication/" + id;
            var result = await GetResultFromApi<WebApplicationDto>(url);
            return result;
        }

        public async Task<WebApplicationDto> PostWebApplication(WebApplicationDto model)
        {
            var url = CRMApiUri + "/WebApplication";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<WebApplicationDto> PutWebApplication(int id,WebApplicationDto model)
        {
            var url = CRMApiUri + "/WebApplication/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<WebApplicationSearchModel> GetWebApplications(WebApplicationSearchModel model)
        {
            var url = CRMApiUri + "/WebApplication/GetWebApplications?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.WebApplicationSearchResult.Clear();
            try
            {
                model.WebApplicationSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<WebApplicationDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(WebApplicationSearchModel searchModel)
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

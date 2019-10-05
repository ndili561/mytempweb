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
    public class MenuItemApiClient : BaseClient, IMenuItemApiClient
    {
        public MenuItemApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<MenuItemDto> GetMenuItem(int id)
        {
            var url = CRMApiUri + "/MenuItem/" + id;
            var result = await GetResultFromApi<MenuItemDto>(url);
            return result;
        }

        public async Task<MenuItemDto> PostMenuItem(MenuItemDto model)
        {
            var url = CRMApiUri + "/MenuItem";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<MenuItemDto> PutMenuItem(int id, MenuItemDto model)
        {
            var url = CRMApiUri + "/MenuItem/" + id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<MenuItemSearchModel> GetMenuItems(MenuItemSearchModel model)
        {
            var url = CRMApiUri + "/MenuItem/GetMenuItems?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.MenuItemSearchResult.Clear();
            try
            {
                model.MenuItemSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<MenuItemDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }



        private string GetFilterString(MenuItemSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (searchModel.ApplicationId > 0)
                {
                    filterString = ODataFilterConstant.Filter + $"ApplicationId eq {searchModel.ApplicationId}";
                    if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                    {
                        if (!string.IsNullOrWhiteSpace(filterString))
                        {
                            filterString += $" and (contains(ControllerName,'{searchModel.FilterText}') eq true";
                            filterString += $" or contains(DisplayName,'{searchModel.FilterText}') eq true )";
                        }
                    }
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}

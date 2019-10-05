using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IWebApplicationApiClient
    {
        Task<WebApplicationSearchModel> GetWebApplications(WebApplicationSearchModel model);
        Task<WebApplicationDto> GetWebApplication(int webApplicationId);
        Task<WebApplicationDto> PostWebApplication(WebApplicationDto model);
        Task<WebApplicationDto> PutWebApplication(int id, WebApplicationDto model);
    }
}
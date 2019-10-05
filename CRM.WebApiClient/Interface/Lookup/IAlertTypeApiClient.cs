using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IAlertTypeApiClient
    {
        Task<AlertTypeSearchModel> GetAlertTypes(AlertTypeSearchModel model);
        Task<AlertTypeDto> GetAlertType(int id);
        Task<AlertTypeDto> PostAlertType(AlertTypeDto model);
        Task<AlertTypeDto> PutAlertType(int id, AlertTypeDto model);
    }
}
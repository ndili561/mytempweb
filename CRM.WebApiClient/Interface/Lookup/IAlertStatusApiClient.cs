using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IAlertStatusApiClient
    {
        Task<AlertStatusSearchModel> GetAlertStatuses(AlertStatusSearchModel model);
        Task<AlertStatusDto> GetAlertStatus(int id);
        Task<AlertStatusDto> PostAlertStatus(AlertStatusDto model);
        Task<AlertStatusDto> PutAlertStatus(int id, AlertStatusDto model);
    }
}
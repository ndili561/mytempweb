using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IAlertGroupApiClient
    {
        Task<AlertGroupSearchModel> GetAlertGroups(AlertGroupSearchModel model);
        Task<AlertGroupDto> GetAlertGroup(int id);
        Task<AlertGroupDto> PostAlertGroup(AlertGroupDto model);
        Task<AlertGroupDto> PutAlertGroup(int id, AlertGroupDto model);
    }
}
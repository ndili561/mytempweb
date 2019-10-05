using System.Threading.Tasks;
using CRM.Entity.Search.Common;

namespace CRM.WebApiClient.Interface.Common
{
    /// <summary>
    /// </summary>
    public interface IRepairApiClient
    {
        Task<RepairSearchModel> GetRepairs(RepairSearchModel model);
    }
}
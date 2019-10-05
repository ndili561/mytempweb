using System.Threading.Tasks;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface ITenantHistoryViewApiClient
    {
        Task<TenantHistoryViewSearchModel> GetTenantHistoryViews(TenantHistoryViewSearchModel model);
    }
}
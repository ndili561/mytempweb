using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface ITenantApiClient
    {
        Task<TenantSearchModel> GetTenants(TenantSearchModel model);
        Task<TenantDto> GetTenantByTenantCode(string tenantCode);
        Task<TenantDto> GetTenant(int id);
        Task<TenantDto> PostTenant(TenantDto model);
        Task<TenantDto> PutTenant(int id, TenantDto model);
    }
}
using System.Collections.Generic;
using System.Linq;
using CRM.DAL.Database.Entities.Customer;

namespace CRM.WebAPI.Services.OData
{
    public interface IOdataTenantService
    {
        IQueryable<Tenant> GetTenant();
        List<TenantHistoryView> GetTenantHistoryViews(string tenantCode);
    }
}

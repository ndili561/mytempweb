using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Customer;
using CRM.WebAPI.Controllers.OData;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.OData
{
    public class OdataTenantService : IOdataTenantService
    {
        readonly CRMContext _crmContext;
        public OdataTenantService(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }
        [EnableQuery]
        public IQueryable<Tenant> GetTenant()
        {
            var result = _crmContext.Tenants.Include(x=>x.Person);
            return result.AsQueryable();
        }

        public List<TenantHistoryView> GetTenantHistoryViews(string tenantCode)
        {
            return _crmContext.TenantHistoryView.Where(x => x.TenantCode == tenantCode).ToList();
        }
    }
}

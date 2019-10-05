using System.Collections.Generic;
using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Customer;
using CRM.WebAPI.Services.OData;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    //bug: Please do not use ROUTE attribute as it add bug to the system
    [Produces("application/json")]
    public class TenantController : ODataBaseController
    {
        public TenantController(CRMContext crmContext) : base(crmContext)
        {
        }

        [EnableQuery]
        public IQueryable<Tenant> Get() => CRMContext.Tenants.AsQueryable();
    }

  

   
}
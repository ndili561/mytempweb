using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    //bug: Please do not use ROUTE attribute as it add bug to the system
    [Produces("application/json")]
    public class ContactTypeController : ODataBaseController
    {
        public ContactTypeController(CRMContext crmContext):base(crmContext)
        {
        }

        [EnableQuery]
        public IQueryable<ContactType> Get() => CRMContext.ContactTypes.AsQueryable();
    }
}
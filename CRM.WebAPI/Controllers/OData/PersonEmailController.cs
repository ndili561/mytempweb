using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    [Produces("application/json")]
    public class PersonEmailController : ODataController
    {
        private readonly CRMContext _crmContext;

        public PersonEmailController(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }

        [EnableQuery]
        public IQueryable<PersonEmail> Get() => _crmContext.PersonEmails.AsQueryable();
    }
}
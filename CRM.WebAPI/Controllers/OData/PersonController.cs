using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    [Produces("application/json")]
    public class PersonController : ODataController
    {
        private readonly CRMContext _crmContext;

        public PersonController(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }

        [EnableQuery]
        public IQueryable<Person> Get() => _crmContext.Persons.AsQueryable();
    }
}
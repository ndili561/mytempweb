using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    [Produces("application/json")]
    public class UserEmailController : ODataController
    {
        private readonly CRMContext _crmContext;

        public UserEmailController(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }

        [EnableQuery]
        public IQueryable<UserEmail> Get() => _crmContext.UserEmails.AsQueryable();
    }
}
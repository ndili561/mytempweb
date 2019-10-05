using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    [Produces("application/json")]
    public class UserSmsController : ODataController
    {
        private readonly CRMContext _crmContext;

        public UserSmsController(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }

        [EnableQuery]
        public IQueryable<UserSms> Get() => _crmContext.UserSmses.AsQueryable();
    }
}
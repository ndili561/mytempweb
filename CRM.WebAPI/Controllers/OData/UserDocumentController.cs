using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    [Produces("application/json")]
    public class UserDocumentController : ODataController
    {
        private readonly CRMContext _crmContext;

        public UserDocumentController(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }

        [EnableQuery]
        public IQueryable<UserDocument> Get() => _crmContext.UserDocuments.AsQueryable();
    }
}
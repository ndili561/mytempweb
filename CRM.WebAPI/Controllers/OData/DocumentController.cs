using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Database.Entities.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Controllers.OData
{

    [Produces("application/json")]
    public class DocumentController : ODataController
    {
        private readonly CRMContext _crmContext;

        public DocumentController(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }

        [EnableQuery]
        public IQueryable<Document> Get() => _crmContext.Documents.AsQueryable();
    }
}
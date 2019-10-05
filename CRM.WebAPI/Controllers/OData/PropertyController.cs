using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    //bug: Please do not use ROUTE attribute as it add bug to the system
    [Produces("application/json")]
    public class PropertyController : ODataBaseController
    {
        public PropertyController(CRMContext crmContext):base(crmContext)
        {
        }

        [EnableQuery]
        public IQueryable<Property> Get() => CRMContext.Properties.AsQueryable();
    }
}
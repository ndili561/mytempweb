using CRM.DAL.Database;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    [Produces("application/json")]
    public class ODataBaseController : ODataController
    {
        protected readonly CRMContext CRMContext;

        public ODataBaseController(CRMContext crmContext)
        {
            CRMContext = crmContext;
        }

       
    }
}
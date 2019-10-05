using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Controllers.OData
{

    //bug: Please do not use ROUTE attribute as it add bug to the system
    //http://azurecoder.net/2018/02/19/creating-odata-api-asp-net-core-2-0/
    //http://localhost:53055/odata/User?$filter=Applications/any(b: b/Id eq 2) &$top=8&$skip=0&$orderby=Id desc&$count=true
    //&$select=Name will only select Name
    //$expand=Applications&$filter=Id eq 1 (will select the user linked with application id 1)
    //http://localhost:5000/odata/$metadata will give the metadata of the relationships
    [Produces("application/json")]
    public class UserController : ODataBaseController
    {

        public UserController(CRMContext crmContext):base(crmContext)
        {
        }

        [EnableQuery]
        public IQueryable<User> Get() => CRMContext.Users.AsQueryable();
    }
}
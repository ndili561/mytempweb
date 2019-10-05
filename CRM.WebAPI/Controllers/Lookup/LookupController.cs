using CRM.Dto.Lookup;
using CRM.WebAPI.BLL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Route("api/[controller]")]
    public class LookupController : Controller
    {
        private readonly ILookupBll _lookupBll;
        public LookupController(ILookupBll lookupBll)
        {
            _lookupBll = lookupBll;

        }
        [HttpGet]
        public LookupDto GetAll()
        {
         return _lookupBll.Get();
        }
        
    }
}
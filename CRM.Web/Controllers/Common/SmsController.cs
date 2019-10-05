using CRM.WebApiClient.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Common

{
    public class SmsController : BaseController
    {
        private readonly ICommonFacadeApiClient _commonFacadeApiClient;
        public SmsController(ICommonFacadeApiClient commonFacadeApiClient)
        {
            _commonFacadeApiClient = commonFacadeApiClient;
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}
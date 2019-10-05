using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CRM.WebApiClient.Interface.Facade;

namespace CRM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGatewayFacadeApiClient _facadeApiClient;

        public HomeController(IGatewayFacadeApiClient facadeApiClient)
        {
            _facadeApiClient = facadeApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync("GatewayCoreCookies");
            await HttpContext.SignOutAsync("GatewayCoreoidc");
            return RedirectToAction("Index");
        }
        public IActionResult ErrorForbidden()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult ErrorNotLoggedIn()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            throw new System.NotImplementedException();
        }
        [HttpGet]
        public IActionResult AjaxGetData()
        {
            try
            {
                var lookup = _facadeApiClient.GetLookup().Result;
                return PartialView("TestGet", lookup);
            }
            catch (Exception e)
            {
                return ManageException(e);
            }
        }

        private IActionResult ManageException(Exception e)
        {
            var sb = new StringBuilder();
            while (e != null)
            {
                sb.AppendLine(e.Message);
                e = e.InnerException;
            }
            return PartialView("_ErrorMessage", sb.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AjaxPostData(int id)
        {
            try
            {
                var lookup = _facadeApiClient.GetLookup().Result;
                return PartialView("TestGet", lookup);
            }
            catch (Exception e)
            {
                return ManageException(e);
            }
        }

        [HttpGet]
        public IActionResult TestDataFromWebServer()
        {
            return PartialView("_ErrorMessage","The GET method is working fine.");
        }
        
        [HttpPost]
        public IActionResult PostDataFromWebServer(string message)
        {
            return RedirectToAction("Details","Test",new{personId= 108414 });
        }
    }
}
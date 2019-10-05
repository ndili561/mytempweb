using CRM.Entity.Model.Customer;
using CRM.WebApiClient.Interface.Customer;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Customer

{
    public class PersonSmsController : BaseController
    {
        private readonly IPersonSmsApiClient _personSmsApiClient;
        public PersonSmsController(IPersonSmsApiClient personSmsApiClient)
        {
            _personSmsApiClient = personSmsApiClient;
        }

        [HttpPost]
        public IActionResult SendEmail(PersonSmsDto model)
        {
            bool success;
            model = _personSmsApiClient.PostPersonSms(model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}
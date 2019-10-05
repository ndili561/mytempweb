using CRM.Entity.Model.Customer;
using CRM.WebApiClient.Interface.Customer;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Customer

{
    public class PersonEmailController : BaseController
    {
        private readonly IPersonEmailApiClient _personEmailApiClient;
        public PersonEmailController(IPersonEmailApiClient personEmailApiClient)
        {
            _personEmailApiClient = personEmailApiClient;
        }

        [HttpPost]
        public IActionResult SendEmail(PersonEmailDto model)
        {
            bool success;
            model = _personEmailApiClient.PostPersonEmail(model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}
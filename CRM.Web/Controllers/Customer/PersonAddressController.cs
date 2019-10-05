using CRM.Entity.Model.Customer;
using CRM.WebApiClient.Interface.Customer;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.Customer

{
    public class PersonAddressController : BaseController
    {
        private readonly IPersonAddressApiClient _personAddressApiClient;
        public PersonAddressController(IPersonAddressApiClient personAddressApiClient)
        {
            _personAddressApiClient = personAddressApiClient;
        }

        [HttpPost]
        public IActionResult Save(PersonAddressDto model)
        {
            bool success;
            model = model.Id == 0 ? _personAddressApiClient.PostPersonAddress(model).Result
                : _personAddressApiClient.PutPersonAddress(model.Id, model).Result;
            success = string.IsNullOrWhiteSpace(model.ErrorMessage);
            return Json(new { success, message = success ? model.SuccessMessage : model.ErrorMessage });
        }
    }
}
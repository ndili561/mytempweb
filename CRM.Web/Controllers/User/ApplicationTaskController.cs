using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.User

{
    public class ApplicationTaskController : BaseController
    {
        private readonly IApplicationTaskApiClient _lookupFacadeApiClient;
        public ApplicationTaskController(IApplicationTaskApiClient lookupFacadeApiClient)
        {
            _lookupFacadeApiClient = lookupFacadeApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private ApplicationTaskSearchModel InitializeModel(ApplicationTaskSearchModel model)
        {
            if (model == null)
            {
                model = new ApplicationTaskSearchModel
                {
                    SortColumn = "Id",
                    SortDirection = "Desc",
                    PageSize = 8,
                    PageNumber = 1
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.SortColumn))
                {
                    model.SortColumn = "Id";
                    model.SortDirection = "Desc";
                }
            }
            model.TargetGridId = "ApplicationTaskGrid";
            return model;
        }
        public IActionResult Grid(ApplicationTaskSearchModel model)
        {
            model = InitializeModel(model);
            var result = _lookupFacadeApiClient.GetApplicationTasks(model).Result;
            return PartialView(result);
        }
       
    }
}
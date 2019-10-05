using System.Linq;
using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers.User

{
   
    public class AuditController : Controller
    {
        private readonly IAuditApiClient _auditApiClient;
        public AuditController(IAuditApiClient auditApiClient)
        {
            _auditApiClient = auditApiClient;
        }
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            _auditApiClient.GetAudits(model); 
            return View(model);
        }
        private AuditSearchModel InitializeModel(AuditSearchModel model)
        {
            if (model == null)
            {
                model = new AuditSearchModel
                {
                    TargetGridId = "AuditGrid",
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
            return model;
        }

        public IActionResult Grid(AuditSearchModel model)
        {
            model = InitializeModel(model);
            var result = _auditApiClient.GetAudits(model).Result;
            model.SelectedTable = !string.IsNullOrWhiteSpace(model.SelectedTable)
                ? model.SelectedTable
                : model.TableList.FirstOrDefault(x => x.ToLower().Contains("please"));
            return PartialView(result);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Entity.Model.Employee;
using CRM.Web.Controllers;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRM.Web.Attribute
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class TraceUserActivity : ActionFilterAttribute
    {
        private BaseController _controller;
        private IUserActivityApiClient _userActivityApiClient;
        private ActionDescriptor CurrentAction { get; set; }
        private IDictionary<string,object> ActionArguments { get; set; }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _userActivityApiClient = context.HttpContext.RequestServices.GetService(typeof(IUserActivityApiClient)) as IUserActivityApiClient;
            PostUserActivities();
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _controller = context.Controller as BaseController;
            CurrentAction = context.ActionDescriptor;
            ActionArguments = context.ActionArguments;
            base.OnActionExecuting(context);
        }

        private void PostUserActivities()
        {
            if (ActionArguments.Any(x => x.Key == "personId"))
            {
                int id;
                if (int.TryParse(ActionArguments.FirstOrDefault().Value?.ToString(), out id) && _controller.CurrentUserContactId > 0)
                {
                    _userActivityApiClient.PostUserActivity(new UserActivityDto { PersonId = id,CreatedOn = DateTime.Now,UserId = _controller.CurrentUserContactId });
                }
            }
        }
    }
}

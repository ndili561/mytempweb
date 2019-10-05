using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Core.Utilities;
using Core.Utilities.Enum;
using CRM.Entity;
using CRM.Entity.Helper;
using CRM.Entity.Model;
using CRM.Web.Attribute;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Web.Controllers
{
   
    [TraceUserActivity]
    public abstract class BaseController : Controller
    {
        private readonly IHostingEnvironment _env;
        protected BaseController()
        {

        }
        protected BaseController(IHostingEnvironment env)
        {
            _env = env;
        }

        protected string ErrMessage;
        protected bool ThumbnailCallback()
        {
            return false;
        }

        protected AuthenticationUser CurrentLoggedUser => new AuthenticationUser { Email = "aa@aa.com", Roles = new[] { "Admin" } };
        public int CurrentUserContactId => CurrentLoggedUser == null || CurrentLoggedUser.ContactId == 0 ? 420 : CurrentLoggedUser.ContactId;
        protected string CurrentUserName => CurrentLoggedUser?.Email.GetFullName();
        protected string CurrentUserEmail => CurrentLoggedUser?.Email;
        protected string CurrentUserRole => string.Join(",", CurrentLoggedUser.Roles.Select(x => x.ToLower()).ToArray());
        protected void UpdateAuditInformation(BaseDto model)
        {
            model.CreatedDate = model.CreatedDate == DateTime.MinValue ? DateTime.Now : model.CreatedDate;
            model.CreatedBy = string.IsNullOrEmpty(model.CreatedBy) ? CurrentUserName : model.CreatedBy;
            model.LastModifiedBy = CurrentUserEmail;
            model.LastModifiedOn = DateTime.Now;
        }
        protected void GetStateSettings(ViewState viewState)
        {
            switch (viewState)
            {
                case ViewState.Create:
                    ViewBag.EditPanelText = "New";
                    ViewBag.EditPanelStyle = "panel-mint";
                    break;
                case ViewState.Write:
                    ViewBag.EditPanelText = "Edit";
                    ViewBag.EditPanelStyle = "panel-purple";
                    break;
                case ViewState.Delete:
                    ViewBag.EditPanelText = "Delete";
                    ViewBag.EditPanelStyle = "panel-danger";
                    break;
                case ViewState.NotFound:
                    ViewBag.EditPanelText = "Edit";
                    ViewBag.EditPanelStyle = "panel-info";
                    break;
                default:
                    ViewBag.EditPanelText = "View";
                    ViewBag.EditPanelStyle = "panel-info";
                    break;
            }
            ViewBag.State = viewState;
        }
        protected Image GetReducedImage(Image resourceImage, int width, int height)
        {
            try
            {
                var callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                var reducedImage = resourceImage.GetThumbnailImage(width, height, callb, IntPtr.Zero);
                return reducedImage;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        protected List<int> GetIntListFromString(string stringIds)
        {
            var idIntList = new List<int>();
            if (string.IsNullOrWhiteSpace(stringIds))
                return idIntList;
            return JsonConvert.DeserializeObject<List<int>>(stringIds);
        }
        protected T InitializeSearchModel<T>(T model,string targetGridId,string sortColumn, string sortDirection) where T: BaseFilterModel
        {
            if (model == null)
            {
                model = Activator.CreateInstance<T>();
                model.PageSize = 8;
                model.PageNumber = 1;
                model.SortColumn = sortColumn;
                model.SortDirection = sortDirection;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.SortColumn))
                {
                    model.SortColumn = "Id";
                    model.SortDirection = "Desc";
                }
            }
          
            model.TargetGridId = targetGridId;
            return model;
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Employee
{
    // This class will hold all the Role name. 
    public class RoleDto : BaseDto
    {
        public RoleDto()
        {
          //  RolePermissions = new HashSet<PermissionDto>();
            ApplicationRoles = new HashSet<ApplicationRoleDto>();
            ReadonlyPermisssionSelectList = new List<SelectListItem>();
            FullAccessPermisssionSelectList= new List<SelectListItem>();
            LinkedFullAccessRolePermissionsIds= new List<string>();
            LinkedReadonlyRolePermissionsIds = new List<string>();
        }

        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsAdministrator { get; set; }
        //public virtual ICollection<PermissionDto> RolePermissions { get; set; }
        //public virtual ICollection<UserApplicationRoleDto> UserRoles { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public int ApplicationId { get; set; }
        [DisplayName("Full Access")]
        public IEnumerable<string> LinkedFullAccessRolePermissionsIds { get; set; }
        [DisplayName("Readonly")]
        public IEnumerable<string> LinkedReadonlyRolePermissionsIds { get; set; }

        public List<SelectListItem> ReadonlyPermisssionSelectList { get; set; }
        public List<SelectListItem> FullAccessPermisssionSelectList { get; set; }
        public virtual ICollection<ApplicationRoleDto> ApplicationRoles { get; set; }
    }
}

using System.Collections.Generic;

namespace CRM.Entity.Model.Employee
{
    public class WebApplicationDto : BaseDto
    {
        public WebApplicationDto()
        {
            ApplicationUsers = new List<ApplicationUserDto>();
            ApplicationRoles = new List<ApplicationRoleDto>();
            MenuItems = new List<MenuItemDto>();
            ApplicationTasks = new List<TaskDto>();
            Roles = new List<RoleDto>();
        }
        public string Name { get; set; }
        public virtual ICollection<ApplicationUserDto> ApplicationUsers { get; set; }
        public virtual ICollection<ApplicationRoleDto> ApplicationRoles { get; set; }
        public virtual ICollection<MenuItemDto> MenuItems { get; set; }
        public virtual ICollection<TaskDto> ApplicationTasks { get; set; }

        public virtual List<RoleDto> Roles { get; set; }
    }
}

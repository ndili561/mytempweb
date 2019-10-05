using System.Collections.Generic;

namespace CRM.Dto.Employee
{
    public class WebApplicationDto : BaseDto
    {
        public string Name { get; set; }
        public virtual ICollection<ApplicationUserDto> ApplicationUsers { get; set; }
        public virtual ICollection<ApplicationRoleDto> ApplicationRoles { get; set; }
        public virtual ICollection<MenuItemDto> MenuItems { get; set; }
        public virtual ICollection<ApplicationTaskDto> ApplicationTasks { get; set; }
    }
}

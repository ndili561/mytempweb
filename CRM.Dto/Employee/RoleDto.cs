using System.Collections.Generic;
using System.ComponentModel;

namespace CRM.Dto.Employee
{
    // This class will hold all the Role name. 
    public class RoleDto : BaseDto
    {
        public RoleDto()
        {
            UserRoles = new HashSet<UserApplicationRoleDto>();
            ApplicationRoles = new HashSet<ApplicationRoleDto>();
        }

        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int ApplicationId { get; set; }
        public bool IsAdministrator { get; set; }
        public virtual ICollection<ApplicationRoleDto> ApplicationRoles { get; set; }
        public virtual ICollection<UserApplicationRoleDto> UserRoles { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}

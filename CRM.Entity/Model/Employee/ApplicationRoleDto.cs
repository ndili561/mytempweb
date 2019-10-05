using System.Collections.Generic;

namespace CRM.Entity.Model.Employee
{
    // This class will hold the link between Role and Application . 
    public class ApplicationRoleDto : BaseDto
    {
        public ApplicationRoleDto()
        {
            RolePermissions = new HashSet<PermissionDto>();
        }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
        public int ApplicationId { get; set; }
        public WebApplicationDto Application { get; set; }
        public virtual ICollection<PermissionDto> RolePermissions { get; set; }
    }
}

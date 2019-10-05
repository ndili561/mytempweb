using CRM.Entity.Enum;

namespace CRM.Entity.Model.Employee
{
    public class PermissionDto : BaseDto
    {
        public int MenuItemId { get; set; }
        public virtual MenuItemDto MenuItem { get; set; }
        public int ApplicationRoleId { get; set; }
        public virtual ApplicationRoleDto ApplicationRole { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public int RoleId { get; set; }
    }
}

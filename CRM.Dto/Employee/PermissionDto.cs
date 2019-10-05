using CRM.Dto.Enum;

namespace CRM.Dto.Employee
{
    public class PermissionDto : BaseDto
    {
        public int MenuItemId { get; set; }
        public virtual MenuItemDto MenuItem { get; set; }
        public int ApplicationRoleId { get; set; }
        public virtual ApplicationRoleDto ApplicationRole { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
    }
}

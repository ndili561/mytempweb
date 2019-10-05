namespace CRM.Dto.Employee
{
    public class UserApplicationRoleDto : BaseDto
    {
        public int UserId { get; set; }
        public virtual UserDto User { get; set; }
        public int ApplicationRoleId { get; set; }
        public virtual ApplicationRoleDto Role { get; set; }
    }
}

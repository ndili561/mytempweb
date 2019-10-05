namespace CRM.Dto.Employee
{
    public class ApplicationUserDto : BaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int ApplicationId { get; set; }
        public WebApplicationDto Application { get; set; }
        public int? ApplicationAccessLevel { get; set; }
    }
}

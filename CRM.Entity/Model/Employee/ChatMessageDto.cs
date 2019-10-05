namespace CRM.Entity.Model.Employee
{
    public class ChatMessageDto : BaseDto
    {
        public int HostId { get; set; }
        public UserDto Host { get; set; }
        public int GuestId { get; set; }
       
        public UserDto Guest { get; set; }
        public string Message { get; set; }
    }
}

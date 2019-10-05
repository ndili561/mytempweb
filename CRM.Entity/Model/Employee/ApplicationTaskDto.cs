namespace CRM.Entity.Model.Employee
{
    // This class will hold the link between Role and Application . 
    public class ApplicationTaskDto : BaseDto
    {
        public int TaskId { get; set; }
        public TaskDto Task { get; set; }
        public int ApplicationId { get; set; }
        public WebApplicationDto Application { get; set; }
    }
}

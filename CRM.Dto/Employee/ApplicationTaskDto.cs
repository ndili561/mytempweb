using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Dto.Employee
{
    public class ApplicationTaskDto : BaseDto
    {
        public int ApplicationId { get; set; }
        public WebApplicationDto Application { get; set; }
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public TaskDto Task { get; set; }
        public string Comment { get; set; }
    }
}

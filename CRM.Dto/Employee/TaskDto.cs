using System.Collections.Generic;
using System.ComponentModel;

using CRM.Dto.Enum;

namespace CRM.Dto.Employee
{
    public class TaskDto : BaseDto
    {
        public string Name { get; set; }
        [DisplayName("Duration")]
        public TaskDuration TaskDuration { get; set; }
        [DisplayName("Css")]
        public string TaskCss { get; set; }
        [DisplayName("Style")]
        public string TaskStyle { get; set; }
        public string Comment { get; set; }
        [DefaultValue(true)]
        [DisplayName("Active?")]
        public bool IsActive { get; set; }
        public bool UpdateLinkedApplication { get; set; }
        public IEnumerable<string> LinkedApplicationTaskIds { get; set; }
        public List<ApplicationTaskDto> ApplicationTasks { get; set; }
        public List<WebApplicationDto> ApplicationNotLinkedWithTasks { get; set; }

    }
}

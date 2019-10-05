using System.Collections.Generic;
using System.ComponentModel;
using CRM.Entity.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Employee
{
    public class TaskDto : BaseDto
    {
        public string Name { get; set; }
        [DisplayName("Duration")]
        public TaskDuration TaskDuration { get; set; }
        [DisplayName("Css")]
        public string TaskCss { get; set; }

        [DisplayName("Background Color")]
        public string BackgroundColor { get; set; }

        private string _taskStyle;

        [DisplayName("Style")]
        public string TaskStyle
        {
            get
            {
                if (_taskStyle != null && BackgroundColor != null && _taskStyle.Contains(BackgroundColor))
                {
                    return _taskStyle;
                }
                if (!string.IsNullOrWhiteSpace(BackgroundColor))
                {
                    return "background-color: " + BackgroundColor + ";";
                }
                return "";
            }
            set => _taskStyle = value;
        }
        public string Comment { get; set; }
       
        public bool IsActive { get; set; }
        public bool UpdateLinkedApplication { get; set; }
        public IEnumerable<string> LinkedApplicationTaskIds { get; set; }
        public List<SelectListItem> ApplicationSelectList { get; set; }
        public List<ApplicationTaskDto> ApplicationTasks { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using CRM.DAL.Enum;

namespace CRM.DAL.Database.Entities.Employee
{
    public class Task : BaseEntity
    {
        public Task()
        {
            ApplicationTasks= new List<ApplicationTask>();
        }
        public string Name { get; set; }
        public TaskDuration TaskDuration { get; set; }
        public string TaskCss { get; set; }
        public string TaskStyle { get; set; }
        public string Comment { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public List<ApplicationTask> ApplicationTasks { get; set; }
    }
}

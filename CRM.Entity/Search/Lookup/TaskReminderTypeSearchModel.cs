using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class TaskReminderTypeSearchModel : BaseFilterModel
    {
        public TaskReminderTypeSearchModel()
        {
            TaskReminderTypeSearchResult = new List<TaskReminderTypeDto>();
        }
        public List<TaskReminderTypeDto> TaskReminderTypeSearchResult { get; set; }

       
    }
}
using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class TaskNotificationTypeSearchModel : BaseFilterModel
    {
        public TaskNotificationTypeSearchModel()
        {
            TaskNotificationTypeSearchResult = new List<TaskNotificationTypeDto>();
        }
        public List<TaskNotificationTypeDto> TaskNotificationTypeSearchResult { get; set; }

       
    }
}
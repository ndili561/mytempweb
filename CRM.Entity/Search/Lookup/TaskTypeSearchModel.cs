using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class TaskTypeSearchModel : BaseFilterModel
    {
        public TaskTypeSearchModel()
        {
            TaskTypeSearchResult = new List<TaskTypeDto>();
        }
        public List<TaskTypeDto> TaskTypeSearchResult { get; set; }

       
    }
}
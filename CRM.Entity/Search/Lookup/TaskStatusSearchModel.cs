using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class TaskStatusSearchModel : BaseFilterModel
    {
        public TaskStatusSearchModel()
        {
            TaskStatusSearchResult = new List<TaskStatusDto>();
        }
        public List<TaskStatusDto> TaskStatusSearchResult { get; set; }

       
    }
}
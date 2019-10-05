using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class TaskSearchModel : BaseFilterModel
    {
        public TaskSearchModel()
        {
           TaskSearchResult = new List<TaskDto>();
        }
        public List<TaskDto> TaskSearchResult { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
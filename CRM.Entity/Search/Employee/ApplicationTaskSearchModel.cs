using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class ApplicationTaskSearchModel : BaseFilterModel
    {
        public ApplicationTaskSearchModel()
        {
           ApplicationTaskSearchResult = new List<ApplicationTaskDto>();
        }
        public List<ApplicationTaskDto> ApplicationTaskSearchResult { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
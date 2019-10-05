using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class WebApplicationSearchModel : BaseFilterModel
    {
        public WebApplicationSearchModel()
        {
           WebApplicationSearchResult = new List<WebApplicationDto>();
        }
        public List<WebApplicationDto> WebApplicationSearchResult { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
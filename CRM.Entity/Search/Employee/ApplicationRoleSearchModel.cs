using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class ApplicationRoleSearchModel : BaseFilterModel
    {
        public ApplicationRoleSearchModel()
        {
           ApplicationRoleSearchResult = new List<ApplicationRoleDto>();
        }
        public List<ApplicationRoleDto> ApplicationRoleSearchResult { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int ApplicationId { get; set; }
        public int RoleId { get; set; }
    }
}
using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserApplicationRoleSearchModel : BaseFilterModel
    {
        public UserApplicationRoleSearchModel()
        {
           UserApplicationRoleSearchResult = new List<UserApplicationRoleDto>();
        }
        public List<UserApplicationRoleDto> UserApplicationRoleSearchResult { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
    }
}
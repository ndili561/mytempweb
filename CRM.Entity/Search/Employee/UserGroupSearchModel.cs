using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserGroupSearchModel : BaseFilterModel
    {
        public UserGroupSearchModel()
        {
           UserGroupSearchResult = new List<UserGroupDto>();
        }
        public List<UserGroupDto> UserGroupSearchResult { get; set; }
    }
}
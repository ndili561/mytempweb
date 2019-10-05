using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserActivitySearchModel : BaseFilterModel
    {
        public UserActivitySearchModel()
        {
           UserActivitySearchResult = new List<UserActivityDto>();
        }
        public List<UserActivityDto> UserActivitySearchResult { get; set; }
    }
}
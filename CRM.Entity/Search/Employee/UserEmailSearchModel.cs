using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserEmailSearchModel : BaseFilterModel
    {
        public UserEmailSearchModel()
        {
            UserEmailSearchResult = new List<UserEmailDto>();
        }
        public List<UserEmailDto> UserEmailSearchResult { get; set; }

    }
  
}
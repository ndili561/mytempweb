using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserSmsSearchModel : BaseFilterModel
    {
        public UserSmsSearchModel()
        {
            UserSmsSearchResult = new List<UserSmsDto>();
        }
        public List<UserSmsDto> UserSmsSearchResult { get; set; }

    }
  
}
using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserDiarySearchModel : BaseFilterModel
    {
        public UserDiarySearchModel()
        {
           UserDiarySearchResult = new List<UserDiaryDto>();
        }
        public List<UserDiaryDto> UserDiarySearchResult { get; set; }
    }
}
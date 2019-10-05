using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserTaskSearchModel : BaseFilterModel
    {
        public UserTaskSearchModel()
        {
           UserTaskSearchResult = new List<UserPersonTaskDto>();
        }
        public List<UserPersonTaskDto> UserTaskSearchResult { get; set; }
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public int ApplicationId { get; set; }
        public int TaskTypeId { get; set; }
    }
}
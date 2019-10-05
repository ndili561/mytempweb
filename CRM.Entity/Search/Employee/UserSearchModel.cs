using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserSearchModel : BaseFilterModel
    {
        public UserSearchModel()
        {
           UserSearchResult = new List<UserDto>();
        }
        public List<UserDto> UserSearchResult { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int TaskId { get; set; }
    }
}
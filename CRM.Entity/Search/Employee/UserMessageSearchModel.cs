using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserMessageSearchModel : BaseFilterModel
    {
        public UserMessageSearchModel()
        {
           UserMessageSearchResult = new List<UserMessageDto>();
        }
        public List<UserMessageDto> UserMessageSearchResult { get; set; }
    }
}
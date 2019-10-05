using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserTaskReminderSearchModel : BaseFilterModel
    {
        public UserTaskReminderSearchModel()
        {
           UserTaskReminderSearchResult = new List<UserTaskReminderDto>();
        }
        public List<UserTaskReminderDto> UserTaskReminderSearchResult { get; set; }
    }
}
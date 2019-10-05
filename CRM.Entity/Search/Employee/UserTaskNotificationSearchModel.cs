using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserTaskNotificationSearchModel : BaseFilterModel
    {
        public UserTaskNotificationSearchModel()
        {
           UserTaskNotificationSearchResult = new List<UserTaskNotificationDto>();
        }
        public List<UserTaskNotificationDto> UserTaskNotificationSearchResult { get; set; }
    }
}
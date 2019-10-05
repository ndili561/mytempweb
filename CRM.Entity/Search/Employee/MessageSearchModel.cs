using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class MessageSearchModel : BaseFilterModel
    {
        public MessageSearchModel()
        {
           MessageSearchResult = new List<MessageDto>();
        }
        public List<MessageDto> MessageSearchResult { get; set; }
    }
}
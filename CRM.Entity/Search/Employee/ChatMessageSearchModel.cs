using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class ChatMessageSearchModel : BaseFilterModel
    {
        public ChatMessageSearchModel()
        {
           ChatMessageSearchResult = new List<ChatMessageDto>();
        }
        public List<ChatMessageDto> ChatMessageSearchResult { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
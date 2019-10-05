using System;

namespace CRM.Dto.Employee
{
    public class AuditDto
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}

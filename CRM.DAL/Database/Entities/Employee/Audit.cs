using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    public class Audit
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

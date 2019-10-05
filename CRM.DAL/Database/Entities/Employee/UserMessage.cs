using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserMessage : BaseEntity
    {
        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public virtual User Recipient { get; set; }
      
        public bool IsRead { get; set; }
        public bool SendSms { get; set; }
        public DateTime? NextReminderDate { get; set; }
      
    }
}

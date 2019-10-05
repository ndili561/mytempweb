using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Dto.Employee
{
    public class UserMessageDto : BaseDto
    {
        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public virtual UserDto Recipient { get; set; }
      
        public bool IsRead { get; set; }
        public bool SendSms { get; set; }
        public DateTime? NextReminderDate { get; set; }
      
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    public class Message : BaseEntity
    {
        public int? ParentMessageId { get; set; }
        public Message ParentMessage { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSms { get; set; }
        public DateTime? NextReminderDate { get; set; }
        public int RaisedById { get; set; }
        [ForeignKey("RaisedById")]
        public virtual User RaisedBy { get; set; }
        public int? UserGroupId { get; set; }
        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }
    }
}

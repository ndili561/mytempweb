using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Common
{
    public class Email : BaseEntity
    {
        public Email()
        {
            UserEmails = new List<UserEmail>();
            PersonEmails = new List<PersonEmail>();
            EmailLabels = new List<EmailLabel>();
        }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        public List<Document> Attachments { get; set; }
        public DateTime? SendOn { get; set; }
        public int? RootMessageId { get; set; }
        public bool SendExternal { get; set; }
        public int? EmailStatusId { get; set; }
        [ForeignKey("EmailStatusId")]
        public EmailStatus EmailStatus { get; set; }
        public int? EmailCategoryId { get; set; }
        [ForeignKey("EmailCategoryId")]
        public EmailCategory EmailCategory { get; set; }
        public virtual ICollection<EmailLabel> EmailLabels { get; set; }
        public virtual ICollection<UserEmail> UserEmails { get; set; }
        public virtual ICollection<PersonEmail> PersonEmails { get; set; }
    }
}

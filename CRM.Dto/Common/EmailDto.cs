using System;
using System.Collections.Generic;
using CRM.Dto.Customer;
using CRM.Dto.Employee;
using CRM.Dto.Lookup;

namespace CRM.Dto.Common
{
    public class EmailDto : BaseDto
    {
        public EmailDto()
        {
            UserEmails = new List<UserEmailDto>();
            PersonEmails = new List<PersonEmailDto>();
            EmailLabels = new List<EmailLabelDto>();
        }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? SenderId { get; set; }
        public UserDto Sender { get; set; }
        public List<DocumentDto> Attachments { get; set; }
        public DateTime? SendOn { get; set; }
        public int? RootMessageId { get; set; }
        public bool SendExternal { get; set; }
        public string Comment { get; set; }
        public int? EmailStatusId { get; set; }
        public EmailStatusDto EmailStatus { get; set; }
        public int? EmailCategoryId { get; set; }
        public EmailCategoryDto EmailCategory { get; set; }
        public virtual ICollection<EmailLabelDto> EmailLabels { get; set; }
        public virtual ICollection<UserEmailDto> UserEmails { get; set; }
        public virtual ICollection<PersonEmailDto> PersonEmails { get; set; }
    }
}

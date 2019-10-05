using System;
using System.Collections.Generic;
using System.ComponentModel;
using CRM.Entity.Model.Customer;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Common
{
    public class EmailDto : BaseDto
    {
        public string ReadCss { get; set; }//"read" or "unread"

        public EmailDto()
        {
            UserEmails = new List<UserEmailDto>();
            PersonEmails = new List<PersonEmailDto>();
            EmailLabels = new List<EmailLabelDto>();
            EmailCategories = new List<EmailCategoryDto>();
            EmailLabelTypes = new List<EmailLabelTypeDto>();
            EmailStatuses = new List<EmailStatusDto>();
            EmailAddresses = new List<SelectListItem>();
            AttachmentFilePaths = new List<string>();
            Attachments= new List<DocumentDto>();
        }
        [DisplayName("To")]
        public string ToEmailAddress { get; set; }
        [DisplayName("Cc")]
        public string CcEmailAddress { get; set; }
        [DisplayName("Bcc")]
        public string BccEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public List<DocumentDto> Attachments { get; set; }
        public List<string> AttachmentFilePaths { get; set; }
        public DateTime? SendOn { get; set; }
        public int? RootMessageId { get; set; }
        public bool SendExternal { get; set; }
        public string Comment { get; set; }
    
       
       
        public virtual ICollection<EmailLabelDto> EmailLabels { get; set; }
        public virtual ICollection<UserEmailDto> UserEmails { get; set; }
        public virtual ICollection<PersonEmailDto> PersonEmails { get; set; }

        [DisplayName("Category")]
        public int? EmailCategoryId { get; set; }
        public EmailCategoryDto EmailCategory { get; set; }
        public virtual ICollection<EmailCategoryDto> EmailCategories { get; set; }
        public virtual ICollection<SelectListItem> EmailCategorySelectListItems { get; set; }

        [DisplayName("Label")]
        public string EmailLabelTypeIds { get; set; }
        public virtual ICollection<EmailLabelTypeDto> EmailLabelTypes { get; set; }
        public virtual ICollection<SelectListItem> EmailLabelTypeSelectListItems { get; set; }

        public int? EmailStatusId { get; set; }
        public EmailStatusDto EmailStatus { get; set; }
        public virtual ICollection<EmailStatusDto> EmailStatuses { get; set; }

        public bool HasAttachment { get; set; }
        public virtual List<SelectListItem> EmailAddresses { get; set; }


        public List<IFormFile> FileAttachment { get; set; }
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}

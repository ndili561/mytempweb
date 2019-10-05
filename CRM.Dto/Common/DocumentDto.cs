using System;
using System.Collections.Generic;
using CRM.Dto.Customer;
using CRM.Dto.Employee;
using CRM.Dto.Lookup;

namespace CRM.Dto.Common
{
    public class DocumentDto : BaseDto
    {
        public DocumentDto()
        {
            UserDocuments = new List<UserDocumentDto>();
            PersonDocuments = new List<PersonDocumentDto>();
        }
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public bool IsActive { get; set; }
        public int? DocumentTypeId { get; set; }
        public DocumentTypeDto DocumentType { get; set; }
        public Byte[] FileContent { get; set; }
        public virtual ICollection<UserDocumentDto> UserDocuments { get; set; }
        public virtual ICollection<PersonDocumentDto> PersonDocuments { get; set; }
        public int? PersonId { get; set; }
        public int? UserId { get; set; }
        public string Comment { get; set; }
        public int UploadById { get; set; }
        public UserDto UploadBy { get; set; }
        public DateTime UploadOn { get; set; }
        public int? EmailId { get; set; }
        public EmailDto Email { get; set; }
    }
}

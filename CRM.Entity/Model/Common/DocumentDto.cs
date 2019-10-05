using System;
using System.Collections.Generic;
using CRM.Entity.Model.Customer;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CRM.Entity.Model.Common
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
        [JsonIgnore]
        public IFormFile UploadedFile { get; set; }
        public byte[] FileContent { get; set; }
        public int? PersonId { get; set; }
        public int? UserId { get; set; }
        public string DocumentTypeName { get; set; }
        public virtual ICollection<UserDocumentDto> UserDocuments { get; set; }
        public virtual ICollection<PersonDocumentDto> PersonDocuments { get; set; }
        public string Comment { get; set; }
        public int UploadById { get; set; }
        public UserDto UploadBy { get; set; }
        public DateTime UploadOn { get; set; }
        public int PropertyId { get; set; }
        public bool IsDefaultImage { get; set; }
        public int ViewOrder { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Common
{
    public class Document : BaseEntity
    {
        public Document()
        {
            UserDocuments = new List<UserDocument>();
            PersonDocuments = new List<PersonDocument>();
            PropertyDocuments = new List<PropertyDocument>();
        }

        public string Name { get; set; }
        public string RelativePath { get; set; }
        public int UploadById { get; set; }
        [ForeignKey("UploadById")]
        public User UploadBy { get; set; }
        public DateTime UploadOn { get; set; }
        public bool IsActive { get; set; }
        public int? DocumentTypeId { get; set; }
        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }
        public int? EmailId { get; set; }
        [ForeignKey("EmailId")]
        public Email Email { get; set; }
        public virtual ICollection<UserDocument> UserDocuments { get; set; }
        public virtual ICollection<PersonDocument> PersonDocuments { get; set; }
        public virtual List<PropertyDocument> PropertyDocuments { get; set; }
        [NotMapped]
        public byte[] FileContent { get; set; }
    }
}

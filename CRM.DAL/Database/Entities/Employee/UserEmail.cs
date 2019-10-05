using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserEmail : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int EmailId { get; set; }
        [ForeignKey("EmailId")]
        public Email Email { get; set; }
        public DateTime? ReadOn { get; set; }
        public string Comment { get; set; }
        public int? EmailStatusId { get; set; }
        [ForeignKey("EmailStatusId")]
        public EmailStatus EmailStatus { get; set; }

        [DefaultValue(false)]
        public bool IsImportant { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}

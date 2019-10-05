using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonCase: AuditBaseEntity
    {
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public string Details { get; set; }
        public DateTime RaisedOn { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User CreateBy { get; set; }
        public int? CaseStatusId { get; set; }
        [ForeignKey("CaseStatusId")]
        public PersonCaseStatus CaseStatus { get; set; }
        public int? CaseTypeId { get; set; }
        [ForeignKey("CaseTypeId")]
        public PersonCaseType CaseType { get; set; }
        public int? PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public Priority Priority { get; set; }
    }
}

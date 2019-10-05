using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonAntiSocialBehaviour: BaseEntity
    {
        public PersonAntiSocialBehaviour()
        {
            PersonAntiSocialBehaviourCaseNotes= new List<PersonAntiSocialBehaviourCaseNote>();
        }

        public string  IBSCaseReference { get; set; }
        public int? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public DateTime LoggedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User CreateBy { get; set; }
        public int? CaseStatusId { get; set; }
        [ForeignKey("CaseStatusId")]
        public AntiSocialBehaviourCaseStatus CaseStatus { get; set; }
        public int? CaseTypeId { get; set; }
        [ForeignKey("CaseTypeId")]
        public AntiSocialBehaviourType CaseType { get; set; }
        public int? CaseClosureReasonId { get; set; }
        [ForeignKey("CaseClosureReasonId")]
        public AntiSocialBehaviourCaseClosureReason CaseClosureReason { get; set; }
        public virtual ICollection<PersonAntiSocialBehaviourCaseNote> PersonAntiSocialBehaviourCaseNotes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using CRM.Dto.Employee;
using CRM.Dto.Lookup;

namespace CRM.Dto.Customer
{
    public class PersonAntiSocialBehaviourDto: BaseDto
    {
        public PersonAntiSocialBehaviourDto()
        {
            PersonAntiSocialBehaviourCaseNotes = new List<PersonAntiSocialBehaviourCaseNoteDto>();
        }
        public string IBSCaseReference { get; set; }
        public int? PersonId { get; set; }
        public PersonDto Person { get; set; }
        public DateTime LoggedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int UserId { get; set; }
        public UserDto CreateBy { get; set; }
        public int? CaseStatusId { get; set; }
        public AntiSocialBehaviourCaseStatusDto CaseStatus { get; set; }
        public int? CaseTypeId { get; set; }
        public AntiSocialBehaviourTypeDto CaseType { get; set; }
        public int? CaseClosureReasonId { get; set; }
        public AntiSocialBehaviourCaseClosureReasonDto CaseClosureReason { get; set; }
        public virtual ICollection<PersonAntiSocialBehaviourCaseNoteDto> PersonAntiSocialBehaviourCaseNotes { get; set; }
    }
}

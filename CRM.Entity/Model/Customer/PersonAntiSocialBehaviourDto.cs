using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Customer
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
        [Display(Name = "Case Status")]
        public int? CaseStatusId { get; set; }
        public List<SelectListItem> CaseStatusSelectListItems { get; set; }
        public AntiSocialBehaviourCaseStatusDto CaseStatus { get; set; }
        [Display(Name = "Case Type")]
        public int? CaseTypeId { get; set; }
        public List<SelectListItem> CaseTypeSelectListItems { get; set; }
        public AntiSocialBehaviourTypeDto CaseType { get; set; }
        [Display(Name = "Closure Reason")]
        public int? CaseClosureReasonId { get; set; }
        public AntiSocialBehaviourCaseClosureReasonDto CaseClosureReason { get; set; }
        public List<SelectListItem> CaseClosureReasonSelectListItems { get; set; }
        public virtual ICollection<PersonAntiSocialBehaviourCaseNoteDto> PersonAntiSocialBehaviourCaseNotes { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        [Display(Name = "Action Date")]
        public DateTime ActionDate { get; set; }
        [Display(Name = "Action Time")]
        public string ActionTime { get; set; }
        public List<SelectListItem> ActionTimeSelectListItems { get; set; }
     
    }
}

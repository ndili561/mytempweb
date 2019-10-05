using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Customer
{
    public class VblApplicationDto: BaseDto
    {
        public VblApplicationDto()
        {
            Contacts = new List<PersonDto>();
        }
        [Display(Name = "Application Id")]
        public int ApplicationId { get; set; }

        public int ApplicationStatusID { get; set; }

        [Display(Name = "Status")]
        public ApplicationStatusDto ApplicationStatus { get; set; }

        public string ApplicationStatusReason { get; set; }
        public int? ApplicationStatusReasonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool? ApplicationEligible { get; set; }
        public DateTime? HOALevelOfNeedDate { get; set; }
        [Display(Name = "Level of need")]
        public int? ApplicationLevelOfNeedID { get; set; }
        [Display(Name = "Reason banding given")]
        public string ApplicationLevelOfNeedReason { get; set; }
        [Display(Name = "Last modified by")]
        public string AssessmentLastModifiedInfo { get; set; }
        public bool? DataProtectionIsPrinted { get; set; }
        public bool? DataProtectionIsSigned { get; set; }
        public bool? DataProtectionConsented { get; set; }
        public int? HOACaseRef { get; set; }

        [StringLength(1000)]
        public string HOAOutcome { get; set; }
        [Display(Name = "HOA contact")]
        public string HOAContact { get; set; }
        public DateTime? HOAOutcomeDate { get; set; }
        public bool? HOACaseIsOpen { get; set; }
        public bool? HOAEligabilitySet { get; set; }
        public int? HOAAppointmentActivityID { get; set; }
        public DateTime? HOAAppointmentScheduledStart { get; set; }
        public int? HOAAppointmentStatusID { get; set; }
        public bool? HOAAppointmentIsAssessor { get; set; }
        public int? VBLSatisfationActivityID { get; set; }
        public DateTime? ApplicationClosedDate { get; set; }
        public DateTime? EarliestMoveDate { get; set; }
        public DateTime? LatestMoveDate { get; set; }
        public string LandLordName { get; set; }
        public bool? LeaveVacantProperty { get; set; }
        public bool? IsSignedDeclarationUploaded { get; set; }
        public bool? MatchToMutualExchage { get; set; }
        public int? MutualExchagePropertyDetailId { get; set; }
        public PersonDto MainApplicant
        {
            get { return Contacts?.FirstOrDefault(x => x.ContactTypeId == 1); }
        }
        public ICollection<PersonDto> Contacts { get; set; }
    }
}

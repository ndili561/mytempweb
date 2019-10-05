using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Database.Entities.Common
{
    public class PropertyVoidView 
    {
        [Key]
        public int? VoidId{ get; set; }
        public string PropertyCode{ get; set; }
        public int? VoidStatusId{ get; set; }
        public DateTime? IBSTenancyEndDate{ get; set; }
        public string TenancyStatus{ get; set; }
        public string TenancyType{ get; set; }
        public string TenancyRef{ get; set; }
        public Guid? SiteManagerUserId{ get; set; }
        public string NHOUserId{ get; set; }
        public bool? IsReadyForMatching{ get; set; }
        public string SignedTerminationReason{ get; set; }
        public string NewTenancyRef{ get; set; }
        public bool? HasCustomerSignedTenancyAgreement{ get; set; }
        public DateTime? CustomerSignUpDate{ get; set; }
        public DateTime? CustomerSignUpIBSTenancyStartDate { get; set; }
        public string CustomerSignUpTenancyType{ get; set; }
        public bool? IsReadyForPropertyShop{ get; set; }
        public DateTime? HoldReleaseDate { get; set; }
        public DateTime? IBSTenancyStartDate { get; set; }
        public int? StarterTenancyID{ get; set; }
        public bool? SendToPropertyShop { get; set; }
        public string LastModifiedById{ get; set; }
        public int? TenancyEndTaskId{ get; set; }
        public int? PropertyInspectionTaskId { get; set; }
        public int? PropertyMatchTaskId { get; set; }
        public int? PropertyHandoverTaskId { get; set; }
        public int? TerminationTaskId { get; set; }
        public int? CustomerViewingTaskId { get; set; }
        public int? CustomerSignupTaskId { get; set; }
        [ConcurrencyCheck, Timestamp]
        public virtual byte[] ConcurrencyCheck { get; set; }
        public bool? FinalInspectionComplete{ get; set; }
        public bool? FinalCleanAndCheckComplete { get; set; }
        public int? SecurityStatusId{ get; set; }
        public bool? PlannedOnDemandHandoverComplete{ get; set; }

    }
}

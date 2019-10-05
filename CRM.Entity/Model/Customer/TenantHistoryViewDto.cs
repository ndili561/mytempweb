using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entity.Model.Customer
{
    public class TenantHistoryViewDto 
    {
        public string PropertyCode { get; set; }
        public string TenancyReference { get; set; }
        [Key]
        public string TenancyPersonReference { get; set; }
        public string TenancyType { get; set; }
        public string TenancyStatus { get; set; }
        public string CurrentBalance { get; set; }
        public string RentGroup { get; set; }
        public string RentGroupDescription { get; set; }
        public DateTime? TenancyStartDate { get; set; }
        public DateTime? TenancyEndDate { get; set; }
        public DateTime? PersonStartDate { get; set; }
        public DateTime? PersonEndDate { get; set; }
        public string IsJointTenant { get; set; }
        [Key]
        public string IsMainTenant { get; set; }
        [Key]
        public string TenantCode { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string Ehhnicity { get; set; }

    }
}

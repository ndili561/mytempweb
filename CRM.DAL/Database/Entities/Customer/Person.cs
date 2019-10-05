using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Customer
{
    public class Person : BaseEntity
    {
        public Person()
        {
            Applications = new List<PersonApplicationLink>();
            Documents = new List<PersonDocument>();
            Emails = new List<PersonEmail>();
            MergePersons = new List<MergePerson>();
            PersonContactDetails = new List<PersonContactDetail>();
            PersonAddresses = new List<PersonAddress>();
            PersonAlerts = new List<PersonAlert>();
            PersonCases = new List<PersonCase>();
            PersonFlags = new List<PersonFlag>();
            PersonAntiSocialBehaviours = new List<PersonAntiSocialBehaviour>();
        }

      

        [MaxLength(50)]
        public string TenantCode { get; set; }//PersonReference in IBS system and PERSON-REF([TBL_CUSTOMER_HISTORIC]) and TENANT_CODE(TBL_CUSTOMER) in MasterReferenceData
        public Guid? GlobalIdentityKey { get; set; }
        public string NationalInsuranceNumber { get; set; } 
        public int? TitleId { get; set; }
        [MaxLength(250)]
        public string Forename { get; set; }
        [MaxLength(100)]
        public string Surname { get; set; }
        public int? ContactTypeId { get; set; }
        [ForeignKey("ContactTypeId")]
        public virtual ContactType ContactType { get; set; }
        public int? MainContactPersonId { get; set; }
        [ForeignKey("MainContactPersonId")]
        public virtual Person MainContactPerson { get; set; }
        public int? TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }
        public int? GenderId { get; set; }
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }
        public int? EthnicityId { get; set; }
        [ForeignKey("EthnicityId")]
        public virtual Ethnicity Ethnicity { get; set; }
        public int? NationalityTypeId { get; set; }
        [ForeignKey("NationalityTypeId")]
        public virtual NationalityType NationalityType { get; set; }
        public int? RelationshipId { get; set; }
        [ForeignKey("RelationshipId")]
        public virtual Relationship Relationship { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? PreferredLanguageId { get; set; }
        [ForeignKey("PreferredLanguageId")]
        public virtual Language PreferredLanguage { get; set; }
        public int? ApplicantTypeId { get; set; }
        [ForeignKey("ApplicantTypeId")]
        public virtual ApplicantType ApplicantType { get; set; }
        public int? ApplicationId { get; set; }
        public string PreferredContactTime { get; set; }
        public virtual ICollection<PersonApplicationLink> Applications { get; set; }
        public virtual ICollection<PersonDocument> Documents { get; set; }
        public virtual ICollection<PersonEmail> Emails { get; set; }
        public virtual ICollection<PersonAddress> PersonAddresses { get; set; }
        public virtual ICollection<MergePerson> MergePersons { get; set; }
        public virtual ICollection<PersonContactDetail> PersonContactDetails { get; set; }
        public virtual ICollection<PersonAlert> PersonAlerts { get; set; }
        public virtual ICollection<PersonCase> PersonCases { get; set; }
        public virtual ICollection<PersonFlag> PersonFlags { get; set; }
        public virtual ICollection<PersonAntiSocialBehaviour> PersonAntiSocialBehaviours { get; set; }
        public bool? IsDuplicate { get; set; }
        public bool? HasDuplicate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Customer
{
    public class PersonDto : BaseDto
    {
        public PersonDto()
        {
            Applications = new List<PersonApplicationLinkDto>();
            Documents = new List<PersonDocumentDto>();
            Emails = new List<PersonEmailDto>();
            MergePersons = new List<MergePersonDto>();
            PersonContactDetails = new List<PersonContactDetailDto>();
            PersonAddresses = new List<PersonAddressDto>();

            HouseholdMembers = new List<PersonDto>();
        }
        [Display(Name = "NI Number")]
        public string NationalInsuranceNumber { get; set; }
        public Guid? GlobalIdentityKey { get; set; }
        public string TenantCode { get; set; }//PersonReference in IBS system and PERSON-REF([TBL_CUSTOMER_HISTORIC]) and TENANT_CODE(TBL_CUSTOMER) in MasterReferenceData
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string DateOfBirthString => DateOfBirth.HasValue ? DateOfBirth.Value.ToShortDateString() : string.Empty;
        public int? ContactTypeId { get; set; }
        public virtual ContactTypeDto ContactType { get; set; }
        public int ContactId { get; set; }
        [Display(Name = "Main Contact Person")]
        public int? MainContactPersonId { get; set; }
        public bool IsMainContactPerson => Id == MainContactPersonId;
        public virtual PersonDto MainContactPerson { get; set; }
        public int? TenantId { get; set; }
        public virtual TenantDto Tenant { get; set; }

        [Display(Name = "Gender")]
        public int? GenderId { get; set; }
        public string Gender { get; set; }
        public List<SelectListItem> GenderSelectList { get; set; }
        [Display(Name = "Nationality")]
        public int? NationalityTypeId { get; set; }
        public string Nationality { get; set; }
        public List<SelectListItem> NationalitySelectList { get; set; }
        [Display(Name = "Relationship")]
        public int? RelationshipId { get; set; }
        public virtual RelationshipDto Relationship { get; set; }
        public List<SelectListItem> RelationshipSelectList { get; set; }
        [Display(Name = "Language")]
        public int? PreferredLanguageId { get; set; }
        public string Language { get; set; }
        public List<SelectListItem> LanguageSelectList { get; set; }

        [Display(Name = "Ethnicity")]
        public int? EthnicityId { get; set; }
        public string Ethnicity { get; set; }
        public IEnumerable<SelectListItem> EthnicitySelectList { get; set; }

        [Display(Name = "Title")]
        public int? TitleId { get; set; }
        public string Title { get; set; }
        public List<SelectListItem> TitleSelectList { get; set; }
        public int? ApplicantTypeId { get; set; }
        public virtual ApplicantTypeDto ApplicantType { get; set; }
        public int? ApplicationId { get; set; }
        public string PreferredContactTime { get; set; }
        public virtual List<PersonApplicationLinkDto> Applications { get; set; }
        public virtual List<PersonDocumentDto> Documents { get; set; }
        public virtual List<PersonEmailDto> Emails { get; set; }
        public virtual List<MergePersonDto> MergePersons { get; set; }
        public virtual List<PersonContactDetailDto> PersonContactDetails { get; set; }
        public virtual List<PersonAddressDto> PersonAddresses { get; set; }
        public virtual List<PersonDto> HouseholdMembers { get; set; }
        public bool? IsDuplicate { get; set; }
        public bool? HasDuplicate { get; set; }
        public List<ContactByOptionDto> ContactByOptions { get; set; }
        public virtual ICollection<PersonAlertDto> PersonAlerts { get; set; }
        public virtual ICollection<PersonCaseDto> PersonCases { get; set; }
        public virtual ICollection<PersonFlagDto> PersonFlags { get; set; }
        public virtual ICollection<PersonAntiSocialBehaviourDto> PersonAntiSocialBehaviours { get; set; }
        public int AssetId => Tenant?.Property?.PropertyDetailView?.AssetId ?? 0;
        public string PeropertyCode => Tenant?.Property?.PropertyCode;
        public VblApplicationDto VblApplication { get; set; }
        public int VblContactId => VblApplication?.MainApplicant?.ContactId??0;
        public bool? IsDeleted { get; set; }

    }
}

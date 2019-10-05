using CRM.Dto.Lookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Dto.Customer
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
            Relationships = new List<RelationshipDto>();
        }
        public Guid? GlobalIdentityKey { get; set; }
        public int? ContactTypeId { get; set; }
        public string TenantCode { get; set; }//PersonReference in IBS system and PERSON-REF([TBL_CUSTOMER_HISTORIC]) and TENANT_CODE(TBL_CUSTOMER) in MasterReferenceData
        public string NationalInsuranceNumber { get; set; }
        public int? TitleId { get; set; }
        [MaxLength(250)]
        public string Forename { get; set; }
        [MaxLength(100)]
        public string Surname { get; set; }
        public int? MainContactPersonId { get; set; }
        public virtual PersonDto MainContactPerson { get; set; }
        public int? ApplicantTypeId { get; set; }
        public virtual ApplicantTypeDto ApplicantType { get; set; }
        public int? ApplicationId { get; set; }
        public int? TenantId { get; set; }
        public virtual TenantDto Tenant { get; set; }
        public int? GenderId { get; set; }
        public virtual GenderDto Gender { get; set; }
        public int? EthnicityId { get; set; }
        public virtual EthnicityDto Ethnicity { get; set; }
        public int? NationalityTypeId { get; set; }
        public virtual NationalityTypeDto NationalityType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? PreferredLanguageId { get; set; }
        public string PreferredContactTime { get; set; }
        public int? RelationshipId { get; set; }
        public virtual LanguageDto PreferredLanguage { get; set; }
        public virtual List<PersonApplicationLinkDto> Applications { get; set; }
        public virtual List<PersonDocumentDto> Documents { get; set; }
        public virtual List<PersonEmailDto> Emails { get; set; }
        public virtual List<MergePersonDto> MergePersons { get; set; }
        public virtual List<PersonContactDetailDto> PersonContactDetails { get; set; }
        public virtual List<PersonAddressDto> PersonAddresses { get; set; }
        public virtual List<RelationshipDto> Relationships { get; set; }
        public bool? IsDuplicate { get; set; }
        public bool? HasDuplicate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

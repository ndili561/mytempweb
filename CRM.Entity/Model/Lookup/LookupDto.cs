using System.Collections.Generic;

namespace CRM.Entity.Model.Lookup
{
    public class LookupDto : BaseDto
    {
        public LookupDto()
        {
            AlertTypes = new List<AlertTypeDto>();
            AlertGroups = new List<AlertGroupDto>();
            AlertStatuses = new List<AlertStatusDto>();
            FlagTypes = new List<FlagTypeDto>();
            FlagGroups = new List<FlagGroupDto>();
            Priorities = new List<PriorityDto>();
            ApplicantTypes = new List<ApplicantTypeDto>();
            Titles = new List<TitleDto>();
            Genders = new List<GenderDto>();
            Ethnicities = new List<EthnicityDto>();
            EmailLabelTypes = new List<EmailLabelTypeDto>();
            EmailCategories = new List<EmailCategoryDto>();
            EmailStatuses = new List<EmailStatusDto>();
            Nationalities = new List<NationalityTypeDto>();
            Languages = new List<LanguageDto>();
            Relationships = new List<RelationshipDto>();
            DocumentTypes = new List<DocumentTypeDto>();
            ContactByOptions = new List<ContactByOptionDto>();
            AntiSocialBehaviourCaseClosureReasons = new List<AntiSocialBehaviourCaseClosureReasonDto>();
            AntiSocialBehaviourCaseStatuses = new List<AntiSocialBehaviourCaseStatusDto>();
            AntiSocialBehaviourTypes = new List<AntiSocialBehaviourTypeDto>();
        }

        public string Name { get; set; }
        public List<AlertTypeDto> AlertTypes { get; set; }
        public List<AlertGroupDto> AlertGroups { get; set; }
        public List<AlertStatusDto> AlertStatuses { get; set; }
        public List<AntiSocialBehaviourCaseClosureReasonDto> AntiSocialBehaviourCaseClosureReasons { get; set; }
        public List<AntiSocialBehaviourCaseStatusDto> AntiSocialBehaviourCaseStatuses { get; set; }
        public List<AntiSocialBehaviourTypeDto> AntiSocialBehaviourTypes { get; set; }

        public List<PersonCaseStatusDto> PersonCaseStatuses { get; set; }
        public List<PersonCaseTypeDto> PersonCaseTypes { get; set; }
        public List<FlagTypeDto> FlagTypes { get; set; }
        public List<FlagGroupDto> FlagGroups { get; set; }
        public List<ApplicantTypeDto> ApplicantTypes { get; set; }
        public List<TitleDto> Titles { get; set; }
        public List<GenderDto> Genders { get; set; }
        public List<EthnicityDto> Ethnicities { get; set; }
        public List<NationalityTypeDto> Nationalities { get; set; }
        public List<RelationshipDto> Relationships { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public List<DocumentTypeDto> DocumentTypes { get; set; }
        public List<EmailLabelTypeDto> EmailLabelTypes { get; set; }
        public List<EmailCategoryDto> EmailCategories { get; set; }
        public List<EmailStatusDto> EmailStatuses { get; set; }
        public List<ContactByOptionDto> ContactByOptions { get; set; }
        public List<PriorityDto> Priorities { get; set; }

    }
}

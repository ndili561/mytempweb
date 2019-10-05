using System.Collections.Generic;

namespace CRM.Dto.Lookup
{
    public class LookupDto : BaseDto
    {
        public LookupDto()
        {
            ApplicantTypes = new List<ApplicantTypeDto>();
            ContactTypes = new List<ContactTypeDto>();
            Titles = new List<TitleDto>();
            Genders = new List<GenderDto>();
            Ethnicities = new List<EthnicityDto>();
            Nationalities = new List<NationalityTypeDto>();
            Languages = new List<LanguageDto>();
            DocumentTypes = new List<DocumentTypeDto>();
            ContactByOptions = new List<ContactByOptionDto>();
            PersonCaseStatuses= new List<PersonCaseStatusDto>();
            PersonCaseTypes= new List<PersonCaseTypeDto>();
            Relationships = new List<RelationshipDto>();
        }
        public List<AlertTypeDto> AlertTypes { get; set; }
        public List<AlertGroupDto> AlertGroups { get; set; }
        public List<FlagTypeDto> FlagTypes { get; set; }
        public List<FlagGroupDto> FlagGroups { get; set; }
        public List<PersonCaseStatusDto> PersonCaseStatuses { get; set; }

        public List<PersonCaseTypeDto> PersonCaseTypes { get; set; }
        public List<ApplicantTypeDto> ApplicantTypes { get; set; }
        public List<ContactTypeDto> ContactTypes { get; set; }
        public List<TitleDto> Titles { get; set; }
        public List<GenderDto> Genders { get; set; }
        public List<EthnicityDto> Ethnicities { get; set; }
        public List<NationalityTypeDto> Nationalities { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public List<DocumentTypeDto> DocumentTypes { get; set; }
        public List<ContactByOptionDto> ContactByOptions { get; set; }
        public List<RelationshipDto> Relationships { get; set; }
        public List<PriorityDto> Priorities { get; set; }
    }
}

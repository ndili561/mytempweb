using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonAntiSocialBehaviourCaseNoteSearchModel : BaseFilterModel
    {
        public PersonAntiSocialBehaviourCaseNoteSearchModel()
        {
            PersonAntiSocialBehaviourCaseNoteSearchResult = new List<PersonAntiSocialBehaviourCaseNoteDto>();
        }
        public List<PersonAntiSocialBehaviourCaseNoteDto> PersonAntiSocialBehaviourCaseNoteSearchResult { get; set; }
        public int PersonId { get; set; }
        public int PersonAntiSocialBehaviourId { get; set; }
    }
}
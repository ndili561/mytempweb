using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonAntiSocialBehaviourSearchModel : BaseFilterModel
    {
        public PersonAntiSocialBehaviourSearchModel()
        {
            PersonAntiSocialBehaviourSearchResult = new List<PersonAntiSocialBehaviourDto>();
        }
        public List<PersonAntiSocialBehaviourDto> PersonAntiSocialBehaviourSearchResult { get; set; }
        public int PersonId { get; set; }
    }
}
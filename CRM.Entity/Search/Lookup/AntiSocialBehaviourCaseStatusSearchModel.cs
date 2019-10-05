using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class AntiSocialBehaviourCaseStatusSearchModel : BaseFilterModel
    {
        public AntiSocialBehaviourCaseStatusSearchModel()
        {
            AntiSocialBehaviourCaseStatusSearchResult = new List<AntiSocialBehaviourCaseStatusDto>();
        }
        public List<AntiSocialBehaviourCaseStatusDto> AntiSocialBehaviourCaseStatusSearchResult { get; set; }

       
    }
}
using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class AntiSocialBehaviourTypeSearchModel : BaseFilterModel
    {
        public AntiSocialBehaviourTypeSearchModel()
        {
            AntiSocialBehaviourTypeSearchResult = new List<AntiSocialBehaviourTypeDto>();
        }
        public List<AntiSocialBehaviourTypeDto> AntiSocialBehaviourTypeSearchResult { get; set; }

       
    }
}
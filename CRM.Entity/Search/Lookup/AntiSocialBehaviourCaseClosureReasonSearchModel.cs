using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class AntiSocialBehaviourCaseClosureReasonSearchModel : BaseFilterModel
    {
        public AntiSocialBehaviourCaseClosureReasonSearchModel()
        {
            AntiSocialBehaviourCaseClosureReasonSearchResult = new List<AntiSocialBehaviourCaseClosureReasonDto>();
        }
        public List<AntiSocialBehaviourCaseClosureReasonDto> AntiSocialBehaviourCaseClosureReasonSearchResult { get; set; }

       
    }
}
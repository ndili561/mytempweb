using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class RelationshipSearchModel : BaseFilterModel
    {
        public RelationshipSearchModel()
        {
            RelationshipSearchResult = new List<RelationshipDto>();
        }
        public List<RelationshipDto> RelationshipSearchResult { get; set; }

       
    }
}
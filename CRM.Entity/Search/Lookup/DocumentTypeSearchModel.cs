using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class DocumentTypeSearchModel : BaseFilterModel
    {
        public DocumentTypeSearchModel()
        {
            DocumentTypeSearchResult = new List<DocumentTypeDto>();
        }
        public List<DocumentTypeDto> DocumentTypeSearchResult { get; set; }

       
    }
}
using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Search.Common
{
    public class PropertyDocumentSearchModel : BaseFilterModel
    {
        public PropertyDocumentSearchModel()
        {
            PropertyDocumentSearchResult = new List<PropertyDocumentDto>();
        }
        public List<PropertyDocumentDto> PropertyDocumentSearchResult { get; set; }
        public int PropertyId { get; set; }
        public int DocumentTypeId { get; set; }
        public List<SelectListItem> DocumentTypeSelectList { get; set; }
    }
}
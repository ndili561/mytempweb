using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Search.Customer
{
    public class PersonDocumentSearchModel : BaseFilterModel
    {
        public PersonDocumentSearchModel()
        {
            PersonDocumentSearchResult = new List<PersonDocumentDto>();
        }
        public List<PersonDocumentDto> PersonDocumentSearchResult { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public int DocumentTypeId { get; set; }
        public List<SelectListItem> DocumentTypeSelectList { get; set; }
    }
  
}
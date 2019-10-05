using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class UserDocumentSearchModel : BaseFilterModel
    {
        public UserDocumentSearchModel()
        {
            UserDocumentSearchResult = new List<UserDocumentDto>();
        }
        public List<UserDocumentDto> UserDocumentSearchResult { get; set; }

    }
  
}
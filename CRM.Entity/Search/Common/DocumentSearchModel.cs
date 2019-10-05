using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class DocumentSearchModel : BaseFilterModel
    {
        public DocumentSearchModel()
        {
           DocumentSearchResult = new List<DocumentDto>();
        }
        public List<DocumentDto> DocumentSearchResult { get; set; }
        public int PersonId { get; set; }
        public int UserId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}
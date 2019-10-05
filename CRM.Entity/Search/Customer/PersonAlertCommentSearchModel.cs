using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonAlertCommentSearchModel : BaseFilterModel
    {
        public PersonAlertCommentSearchModel()
        {
            PersonAlertCommentSearchResult = new List<PersonAlertCommentDto>();
        }
        public List<PersonAlertCommentDto> PersonAlertCommentSearchResult { get; set; }
        public int PersonId { get; set; }
        public int PersonAlertId { get; set; }
    }
}
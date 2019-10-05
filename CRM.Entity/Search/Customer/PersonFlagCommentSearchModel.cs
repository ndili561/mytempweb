using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Search.Customer
{
    public class PersonFlagCommentSearchModel : BaseFilterModel
    {
        public PersonFlagCommentSearchModel()
        {
            PersonFlagCommentSearchResult = new List<PersonFlagCommentDto>();
        }
        public List<PersonFlagCommentDto> PersonFlagCommentSearchResult { get; set; }
        public int PersonId { get; set; }
        public int PersonFlagId { get; set; }
    }
}
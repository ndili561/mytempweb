using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class EmailSearchModel : BaseFilterModel
    {
        public EmailSearchModel()
        {
            EmailSearchResult = new List<EmailDto>();
            EmailDto = new EmailDto();
        }
        public List<EmailDto> EmailSearchResult { get; set; }
        public int PersonId { get; set; }
        public int UserId { get; set; }
        public EmailDto EmailDto { get; set; }
    }
}
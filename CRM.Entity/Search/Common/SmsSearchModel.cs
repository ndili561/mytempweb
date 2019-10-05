using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class SmsSearchModel : BaseFilterModel
    {
        public SmsSearchModel()
        {
            SmsSearchResult = new List<SmsDto>();
        }
        public List<SmsDto> SmsSearchResult { get; set; }
        public int PersonId { get; set; }
        public int UserId { get; set; }
    }
}
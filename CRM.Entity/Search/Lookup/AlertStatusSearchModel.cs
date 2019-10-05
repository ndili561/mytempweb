using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Search.Lookup
{
    public class AlertStatusSearchModel : BaseFilterModel
    {
        public AlertStatusSearchModel()
        {
            AlertStatusSearchResult = new List<AlertStatusDto>();
        }
        public List<AlertStatusDto> AlertStatusSearchResult { get; set; }

       
    }
}
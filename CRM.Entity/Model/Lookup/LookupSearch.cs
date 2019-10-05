using System.Collections.Generic;

namespace CRM.Entity.Model.Lookup
{
    public class LookupSearch
    {
        public string odatacontext { get; set; }
        public LookupDto[] value { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonContactDetail : BaseEntity
    {
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public int ContactByOptionId { get; set; }
        [ForeignKey("ContactByOptionId")]
        public virtual ContactByOption ContactByOption { get; set; }
        public string ContactValue { get; set; }
        public int? PriorityOrder { get; set; }
        public bool? IsDefault { get; set; }
        public string Comment { get; set; }
    }
}

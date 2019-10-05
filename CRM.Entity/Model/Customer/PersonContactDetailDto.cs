using System.ComponentModel.DataAnnotations.Schema;
using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Model.Customer
{
    public class PersonContactDetailDto : BaseDto
    {
        public int PersonId { get; set; }
        public virtual PersonDto Person { get; set; }

        public int ContactByOptionId { get; set; }
        [ForeignKey("ContactByOptionId")]
        public virtual ContactByOptionDto ContactByOption { get; set; }
        public string ContactValue { get; set; }
        public int? PriorityOrder { get; set; }
        public bool? IsDefault { get; set; }
        public string Comment { get; set; }
        public bool IsSelected { get; set; }
    }
}

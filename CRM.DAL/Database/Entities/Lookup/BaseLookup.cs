using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Lookup
{
    public abstract class BaseLookup : BaseEntity
    {
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        public int SortOrder { get; set; }
        
        public bool IsActive { get; set; }
        public int? LookupId { get; set; }
        [ForeignKey("LookupId")]
        public Lookup Lookup { get; set; }
    }
}

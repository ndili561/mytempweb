using System.ComponentModel.DataAnnotations;

namespace CRM.Entity.Model.Lookup
{
    public abstract class BaseLookupDto: BaseDto
    {
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        public int SortOrder { get; set; }
        
        public bool IsActive { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Database.Entities.Lookup
{
    public abstract class BaseLookupCss : BaseLookup
    {
        
        [MaxLength(20)]
        public string CssClass { get; set; }
        [MaxLength(50)]
        public string CssStyle { get; set; }
    }
}

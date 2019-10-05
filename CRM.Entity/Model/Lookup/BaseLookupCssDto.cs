using System.ComponentModel.DataAnnotations;

namespace CRM.Entity.Model.Lookup
{
    public abstract class BaseLookupCssDto : BaseLookupDto
    {
        [MaxLength(20)]
        [Display(Name = "Css Class")]
        public string CssClass { get; set; }
        [MaxLength(50)]
        [Display(Name = "Css Style")]
        public string CssStyle { get; set; }
    }
}

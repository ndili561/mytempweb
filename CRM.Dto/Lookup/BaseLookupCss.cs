using System.ComponentModel.DataAnnotations;

namespace CRM.Dto.Lookup
{
    public abstract class BaseLookupCss : BaseLookupDto
    {

        [MaxLength(20)]
        [Display(Name = "Css Class")]
        public string CssClass { get; set; }
        [MaxLength(50)]
        [Display(Name = "Css Style")]
        public string CssStyle { get; set; }
    }
}

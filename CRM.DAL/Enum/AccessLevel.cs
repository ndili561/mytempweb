using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Enum
{
    public enum AccessLevel
    {
        [Display(Name = "Readonly"),  Description("warning")]
        Readonly = 0,
        [Display(Name = "Full"), Description("success")]
        Full = 10
    }
}

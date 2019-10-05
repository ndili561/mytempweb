using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Enum
{
    public enum PropertyInterestStatus
    {
        [Display(Name = "Not Selected"), Description("primary")]
        NotSelected = 0,
        [Display(Name = "Interested"), Description("success")]
        Interested = 10,
        [Display(Name = "Not Interested"), Description("danger")]
        NotInterested =20,
        [Display(Name = "Reconsider"), Description("warning")]
        Reconsider = 30
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Utilities.Enum
{
    public enum EventType
    {
          
        [Display(Name = "Added"), Description("Active")]
        Insert = 10,

        [Display(Name = "Deleted"), Description("Deleted")]
        Deleted = 20,

        [Display(Name = "Modified"), Description("Modified")]
        Update = 30
    }
}
using System.ComponentModel.DataAnnotations;

namespace CRM.Entity.Enum
{
    public enum TaskCss
    {
        [Display(Name = "primary")]
        Primary = 0,

        [Display(Name = "purple")]
        Purple = 10,

        [Display(Name = "mint")]
        Mint = 20,

        [Display(Name = "warning")]
        Warning = 30,

        [Display(Name = "danger")]
        Danger = 40,

        [Display(Name = "success")]
        Success = 50,

        [Display(Name = "dark")]
        Dark = 60,

        [Display(Name = "pink")]
        Pink = 70,

        [Display(Name = "info")]
        Info = 80
    }
}

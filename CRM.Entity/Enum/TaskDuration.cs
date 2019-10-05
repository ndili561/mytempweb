using System.ComponentModel.DataAnnotations;

namespace CRM.Entity.Enum
{
    public enum TaskDuration
    {
        [Display(Name = "Unknown")]
        Unknown = 0,

        [Display(Name = "Custom")]
        Custom = 1,

        [Display(Name = "Thirty Minute")]
        ThirtyMinute = 30,

        [Display(Name = "One Hour")]
        OneHour = 60,

        [Display(Name = "Two Hours")]
        TwoHours = 120,

        [Display(Name = "Three Hours")]
        ThreeHours = 180,

        [Display(Name = "Four Hours")]
        FourHours = 240,

        [Display(Name = "Five Hours")]
        FiveHours = 300,

        [Display(Name = "Six Hours")]
        SixHours = 360,

        [Display(Name = "Seven Hours")]
        SevenHours = 420,

        [Display(Name = "Full Day")]
        FullDay = 480,
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Model.Employee
{
    /// <summary>
    /// This class contains the name of class usually Web Controller readonly/full/Noaccess access to the user.
    /// </summary>
    public  class MenuItemDto : BaseDto
    {
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public WebApplicationDto Application { get; set; }
        [Required]
        [StringLength(50)]
        public string ControllerName { get; set; }
        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}

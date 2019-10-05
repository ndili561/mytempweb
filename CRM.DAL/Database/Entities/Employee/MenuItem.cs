using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    /// <summary>
    /// This class contains the name of class usually Web Controller readonly/full/Noaccess access to the user.
    /// </summary>
    public  class MenuItem : BaseEntity
    {
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public WebApplication Application { get; set; }
        [Required]
        [StringLength(50)]
        public string ControllerName { get; set; }
        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}

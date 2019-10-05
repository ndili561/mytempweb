using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    public class ApplicationUser : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public WebApplication Application { get; set; }
        public int? ApplicationAccessLevel { get; set; }
    }
}

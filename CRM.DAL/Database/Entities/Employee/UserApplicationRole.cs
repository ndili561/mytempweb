using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserApplicationRole : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int ApplicationRoleId { get; set; }
        [ForeignKey("ApplicationRoleId")]
        public virtual ApplicationRole Role { get; set; }
    }
}

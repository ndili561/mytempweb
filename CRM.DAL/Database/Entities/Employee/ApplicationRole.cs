using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    // This class will hold the link between Role and Application . 
    public class ApplicationRole : BaseEntity
    {
        public ApplicationRole()
        {
            RolePermissions = new HashSet<Permission>();
        }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public WebApplication Application { get; set; }
       
        public string  Comment { get; set; }
        public virtual ICollection<Permission> RolePermissions { get; set; }
    }
}

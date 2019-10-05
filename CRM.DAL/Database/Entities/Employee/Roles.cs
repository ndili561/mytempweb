using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Database.Entities.Employee
{
    // This class will hold all the Role name. 
    public class Role : BaseEntity
    {
        public Role()
        {
            ApplicationRoles = new HashSet<ApplicationRole>();
        }

        [Required, MaxLength(150)]
        public string RoleName { get; set; }
        [Required, MaxLength(500)]
        public string RoleDescription { get; set; }
        public bool IsAdministrator { get; set; }
      
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}

using System.Collections.Generic;

namespace CRM.DAL.Database.Entities.Employee
{
    public class WebApplication : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual ICollection<ApplicationTask> ApplicationTasks { get; set; }
    }
}

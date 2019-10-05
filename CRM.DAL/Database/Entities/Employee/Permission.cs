using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Enum;

namespace CRM.DAL.Database.Entities.Employee
{
    // This is the link table between menu item and Role
    public class Permission : BaseEntity
    {
        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }
        public int ApplicationRoleId { get; set; }
        [ForeignKey("ApplicationRoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
    }
}

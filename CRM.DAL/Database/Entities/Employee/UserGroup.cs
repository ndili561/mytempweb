using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserGroup : BaseEntity
    {

        [Required]
        public string Name { get; set; }

        public string EmailAddress { get; set; }

       

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<User> Users { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}

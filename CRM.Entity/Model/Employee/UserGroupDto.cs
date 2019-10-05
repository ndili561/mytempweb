using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entity.Model.Employee
{
    public class UserGroupDto : BaseDto
    {

        [Required]
        public string Name { get; set; }
        [Display(Name = "Group Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public virtual ICollection<MessageDto> Messages { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}

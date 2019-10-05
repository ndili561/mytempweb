using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Dto.Employee
{
    public class UserGroupDto : BaseDto
    {

        [Required]
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public virtual ICollection<MessageDto> Messages { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
        public bool IsActive { get; set; }
    }
}

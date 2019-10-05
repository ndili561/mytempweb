using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Dto.Employee
{
    public class UserDto:BaseDto
    {
        public UserDto()
        {
            Tasks = new List<UserPersonTaskDto>();
            Roles = new List<UserApplicationRoleDto>();
            Messages = new List<UserMessageDto>();
            Applications = new List<ApplicationUserDto>();
            Activities = new List<UserActivityDto>();
            Diaries = new List<UserDiaryDto>();
        }

        [MaxLength(250)]
        public string Name { get; set; }

        public string EmployeeRef { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }
       
        public string Email { get; set; }
        public int? ManagerId { get; set; }
        public UserDto Manager { get; set; }

        public int? UserGroupId { get; set; }
        public UserGroupDto UserGroup { get; set; }
     
        public bool IsActive { get; set; }

        public bool IsSysAdmin { get; set; }
        public int? PersonId { get; set; }
       
        public string Subject { get; set; }// User uqinue GUID in Identity database
        public virtual ICollection<UserPersonTaskDto> Tasks { get; set; }
        public virtual ICollection<UserApplicationRoleDto> Roles { get; set; }
        public virtual ICollection<UserMessageDto> Messages { get; set; }
        public virtual ICollection<ApplicationUserDto> Applications { get; set; }
        public virtual ICollection<UserActivityDto> Activities { get; set; }
        public virtual ICollection<UserDiaryDto> Diaries { get; set; }
        public byte[] FileContent { get; set; }
    }
}

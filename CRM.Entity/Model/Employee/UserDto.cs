using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CRM.Entity.Model.Employee
{
    public class UserDto:BaseDto
    {
        public UserDto()
        {
            ManagerSelectList = new List<SelectListItem>();
            UserGroupSelectList = new List<SelectListItem>();
            RoleSelectList = new List<SelectListItem>();
            Tasks = new List<UserPersonTaskDto>();
            Roles = new List<UserApplicationRoleDto>();
            Messages = new List<UserMessageDto>();
            Applications = new List<ApplicationUserDto>();
            Activities = new List<UserActivityDto>();
            Diaries = new List<UserDiaryDto>();
        }
        [Required]
        public string Name { get; set; }

        public string EmployeeRef { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }
       
        public string Email { get; set; }

        [DisplayName("Manager")]
        public int? ManagerId { get; set; }
      
        public UserDto Manager { get; set; }
        [DisplayName("Group")]
        public int? UserGroupId { get; set; }
      
        public UserGroupDto UserGroup { get; set; }
     
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [DefaultValue(false)]
        public bool IsSysAdmin { get; set; }
        public int? PersonId { get; set; }
       
        public string subject { get; set; }// User uqinue GUID in Identity database
        public virtual ICollection<UserPersonTaskDto> Tasks { get; set; }
        public virtual ICollection<UserApplicationRoleDto> Roles { get; set; }
        public virtual ICollection<UserMessageDto> Messages { get; set; }
        public virtual ICollection<ApplicationUserDto> Applications { get; set; }
        public virtual ICollection<UserActivityDto> Activities { get; set; }
        public virtual ICollection<UserDiaryDto> Diaries { get; set; }

        public DateTime CalendarStartDate { get; set; }
        public DateTime CalendarEndDate { get; set; }
        public string CalendarViewType { get; set; }
        public List<SelectListItem> ManagerSelectList { get; set; }
        public List<SelectListItem> UserGroupSelectList { get; set; }
        public IEnumerable<string> LinkedUserApplicationIds { get; set; }
        public List<SelectListItem> ApplicationSelectList { get; set; }
        public List<SelectListItem> RoleSelectList { get; set; }
        [DisplayName("Linked application(s)")]
        public string LinkedApplicationIds { get; set; }

        public IEnumerable<string> LinkedUserApplicationRoleIds { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        [JsonIgnore]
        public IFormFile UploadedFile { get; set; }
    }
}

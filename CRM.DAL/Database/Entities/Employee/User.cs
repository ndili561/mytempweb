using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    public class User:BaseEntity
    {
        public User()
        {
            Tasks = new List<UserPersonTask>();
            Roles = new List<UserApplicationRole>();
            Messages = new List<UserMessage>();
            Applications = new List<ApplicationUser>();
            Activities = new List<UserActivity>();
            Diaries = new List<UserDiary>();
            Documents = new List<UserDocument>();
        }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public string EmployeeRef { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }
       
        public string Email { get; set; }
        public int? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public User Manager { get; set; }

        public int? UserGroupId { get; set; }
        [ForeignKey("UserGroupId")]
        public UserGroup UserGroup { get; set; }
     
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [DefaultValue(false)]
        public bool IsSysAdmin { get; set; }
        public int? PersonId { get; set; }
       
        public string Subject { get; set; }// User uqinue GUID in Identity database
        public virtual ICollection<UserPersonTask> Tasks { get; set; }
        public virtual ICollection<UserApplicationRole> Roles { get; set; }
        public virtual ICollection<UserMessage> Messages { get; set; }
        public virtual ICollection<ApplicationUser> Applications { get; set; }
        public virtual ICollection<UserActivity> Activities { get; set; }
        public virtual ICollection<UserDiary> Diaries { get; set; }
        public virtual ICollection<UserDocument> Documents { get; set; }

    }
}

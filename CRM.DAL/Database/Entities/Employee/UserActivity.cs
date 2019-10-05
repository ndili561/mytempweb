using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Customer;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserActivity : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

    }
}

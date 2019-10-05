using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Model.Employee
{
    public class UserActivityDto : BaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}

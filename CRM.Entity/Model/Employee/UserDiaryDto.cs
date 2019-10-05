using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Model.Employee
{
    public class UserDiaryDto: BaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int? PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}

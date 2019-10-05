using System;
using CRM.Dto.Customer;

namespace CRM.Dto.Employee
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

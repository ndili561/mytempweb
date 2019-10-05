using System.Collections.Generic;
using CRM.Dto.Customer;
using CRM.Dto.Employee;

namespace CRM.Dto.Common
{
    public class SmsDto : BaseDto
    {
        public SmsDto()
        {
            UserSmses = new List<UserSmsDto>();
            PersonSmses = new List<PersonSmsDto>();
        }
        public string ReceiverNumber { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public UserDto Sender { get; set; }
        public virtual ICollection<UserSmsDto> UserSmses { get; set; }
        public virtual ICollection<PersonSmsDto> PersonSmses { get; set; }
        public string Comment { get; set; }
    }
}

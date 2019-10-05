using System.ComponentModel;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Model.Common
{
    public class SmsDto : BaseDto
    {
        [DisplayName("Mobile Number")]
        public string ReceiverNumber { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public UserDto Sender { get; set; }
    }
}

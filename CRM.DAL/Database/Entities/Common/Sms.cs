using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;

namespace CRM.DAL.Database.Entities.Common
{
    public class Sms : BaseEntity
    {
        public Sms()
        {
            UserSmses = new List<UserSms>();
            PersonSmses = new List<PersonSms>();
        }
        public string ReceiverNumber { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        public virtual ICollection<UserSms> UserSmses { get; set; }
        public virtual ICollection<PersonSms> PersonSmses { get; set; }
    }
}

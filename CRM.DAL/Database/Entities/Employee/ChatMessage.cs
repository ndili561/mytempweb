using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    public class ChatMessage : BaseEntity
    {
        public int HostId { get; set; }
        [ForeignKey("HostId")]
        public User Host { get; set; }
        public int GuestId { get; set; }
       
        [ForeignKey("GuestId")]
        public User Guest { get; set; }
        public string Message { get; set; }
    }
}

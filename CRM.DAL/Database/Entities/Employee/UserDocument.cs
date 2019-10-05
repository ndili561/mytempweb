using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Common;

namespace CRM.DAL.Database.Entities.Employee
{
    public class UserDocument : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        public string Comment { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Employee;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonApplicationLink : BaseEntity
    {
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public virtual WebApplication Application { get; set; }
        public string ExternalLinkId { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}

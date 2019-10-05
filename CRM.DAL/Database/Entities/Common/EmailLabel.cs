using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Common
{
    public class EmailLabel : BaseEntity
    {
        public int EmailId { get; set; }
        [ForeignKey("EmailId")]
        public virtual Email Email { get; set; }

        public int EmailLabelTypeId { get; set; }
        [ForeignKey("EmailLabelTypeId")]
        public virtual EmailLabelType EmailLabelType { get; set; }

    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Common
{
    public class PropertyVoid : BaseEntity
    {
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
        public int VoidId { get; set; }
        [NotMapped]
        public List<PropertyVoidView> PropertyVoids { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Common;

namespace CRM.DAL.Database.Entities.Customer
{
    public class Tenant : BaseEntity
    {
        public Tenant()
        {
            TenantHistoryViews = new List<TenantHistoryView>();
        }
        [MaxLength(50)]
        public string TenantCode { get; set; }
        [MaxLength(50)]
        public string TenancyReference { get; set; }
        [MaxLength(50)]
        public string TenancyType { get; set; }
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        [NotMapped]
        public List<TenantHistoryView> TenantHistoryViews { get; set; }
    }
}

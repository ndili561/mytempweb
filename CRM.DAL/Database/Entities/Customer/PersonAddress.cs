using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonAddress : BaseEntity
    {
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public int AddressTypeId { get; set; }
        [ForeignKey("AddressTypeId")]
        public virtual AddressType AddressType { get; set; }

        public DateTime? LivingSince { get; set; }
        public DateTime? LivingTill { get; set; }
    }
}

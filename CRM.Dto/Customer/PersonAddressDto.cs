using System;
using CRM.Dto.Lookup;

namespace CRM.Dto.Customer
{
    public class PersonAddressDto : BaseDto
    {
        public int PersonId { get; set; }
        public virtual PersonDto Person { get; set; }

        public int AddressId { get; set; }
        public virtual AddressDto Address { get; set; }
        public int AddressTypeId { get; set; }
        public virtual AddressTypeDto AddressType { get; set; }

        public DateTime? LivingSince { get; set; }
        public DateTime? LivingTill { get; set; }
    }
}

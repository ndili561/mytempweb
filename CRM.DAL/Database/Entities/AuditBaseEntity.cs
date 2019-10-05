using System;

namespace CRM.DAL.Database.Entities
{
    public abstract class AuditBaseEntity: BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}

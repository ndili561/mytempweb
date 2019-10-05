using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Common;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonDocument : AuditBaseEntity
    {
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        public bool IsAntiSocialBehaviour { get; set; }
        public string Comment { get; set; }
    }
}

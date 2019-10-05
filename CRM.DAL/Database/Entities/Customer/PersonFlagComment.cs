using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonFlagComment : AuditBaseEntity
    {
        public int PersonFlagId { get; set; }
        [ForeignKey("PersonFlagId")]
        public PersonFlag PersonFlag { get; set; }
        public string Notes { get; set; }
        
    }
}

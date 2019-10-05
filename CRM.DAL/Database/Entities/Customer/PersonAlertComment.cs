using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonAlertComment : AuditBaseEntity
    {
        public int PersonAlertId { get; set; }
        [ForeignKey("PersonAlertId")]
        public PersonAlert PersonAlert { get; set; }
        public string Notes { get; set; }
     
    }
}

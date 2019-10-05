using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonAlert : AuditBaseEntity
    {
        public PersonAlert()
        {
            PersonAlertComments = new List<PersonAlertComment>();
        }
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public string Details { get; set; }
     
        public int? AlertStatusId { get; set; }
        [ForeignKey("AlertStatusId")]
        public AlertStatus AlertStatus { get; set; }
        public int? AlertTypeId { get; set; }
        [ForeignKey("AlertTypeId")]
        public AlertType AlertType { get; set; }
        public int? AlertGroupId { get; set; }
        [ForeignKey("AlertGroupId")]
        public AlertGroup AlertGroup { get; set; }

        public List<PersonAlertComment> PersonAlertComments { get; set; }
    }
}

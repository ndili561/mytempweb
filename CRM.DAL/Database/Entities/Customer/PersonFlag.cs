using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonFlag : AuditBaseEntity
    {
        public PersonFlag()
        {
            PersonFlagComments = new List<PersonFlagComment>();
        }
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public string Details { get; set; }
        public DateTime RaisedOn { get; set; }
        public int? FlagTypeId { get; set; }
        [ForeignKey("FlagTypeId")]
        public FlagType FlagType { get; set; }
        public int? FlagGroupId { get; set; }
        [ForeignKey("FlagGroupId")]
        public FlagGroup FlagGroup { get; set; }
        public bool IsActive { get; set; }
        public List<PersonFlagComment> PersonFlagComments { get; set; }
    }
}

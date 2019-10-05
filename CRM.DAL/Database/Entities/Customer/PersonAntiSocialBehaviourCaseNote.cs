using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PersonAntiSocialBehaviourCaseNote : BaseEntity
    {
        public int PersonAntiSocialBehaviourId { get; set; }
        [ForeignKey("PersonAntiSocialBehaviourId")]
        public PersonAntiSocialBehaviour PersonAntiSocialBehaviour { get; set; }
        public string Note { get; set; }
        public DateTime? ActionDateTime { get; set; }
    }
}

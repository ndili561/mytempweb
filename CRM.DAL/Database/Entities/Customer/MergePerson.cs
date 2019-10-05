using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Customer
{
    public class MergePerson : BaseEntity
    {
        public int CorrectPersonId { get; set; }
        [ForeignKey("CorrectPersonId")]
        public Person CorrectPerson { get; set; }
        public int DuplicatePersonId { get; set; }
        public bool IsMerged { get; set; }
    }
}

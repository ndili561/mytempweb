namespace CRM.Entity.Model.Customer
{
    public class MergePerson : BaseDto
    {
        public int CorrectPersonId { get; set; }
        public PersonDto CorrectPerson { get; set; }
        public int DuplicatePersonId { get; set; }
        public bool IsMerged { get; set; }
    }
}

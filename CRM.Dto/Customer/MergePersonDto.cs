namespace CRM.Dto.Customer
{
    public class MergePersonDto : BaseDto
    {
        public int CorrectPersonId { get; set; }
        public PersonDto CorrectPerson { get; set; }
        public int DuplicatePersonId { get; set; }
        public bool IsMerged { get; set; }
    }
}

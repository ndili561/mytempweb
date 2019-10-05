namespace CRM.Dto.Customer
{
    public class PersonFlagCommentDto : BaseDto
    {
        public int PersonFlagId { get; set; }
       
        public PersonFlagDto PersonFlag { get; set; }
        public string Notes { get; set; }
    }
}

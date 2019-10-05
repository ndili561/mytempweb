namespace CRM.Entity.Model.Customer
{
    public class PersonFlagCommentDto : BaseDto
    {
        public int PersonFlagId { get; set; }
       
        public PersonFlagDto PersonFlag { get; set; }
        public string Notes { get; set; }
    }
}

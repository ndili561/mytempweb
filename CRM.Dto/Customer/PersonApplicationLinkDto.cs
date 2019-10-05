using CRM.Dto.Employee;

namespace CRM.Dto.Customer
{
    public class PersonApplicationLinkDto : BaseDto
    {
        public int PersonId { get; set; }
        public virtual PersonDto Person { get; set; }
        public int ApplicationId { get; set; }
        public virtual WebApplicationDto Application { get; set; }
        public string ExternalLinkId { get; set; }
        public bool IsActive { get; set; }
    }
}

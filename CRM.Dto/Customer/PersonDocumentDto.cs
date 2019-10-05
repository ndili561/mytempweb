using CRM.Dto.Common;

namespace CRM.Dto.Customer
{
    public class PersonDocumentDto : BaseDto
    {
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public int DocumentId { get; set; }
        public DocumentDto Document { get; set; }
        public string Comment { get; set; }
        public bool IsAntiSocialBehaviour { get; set; }
    }
}

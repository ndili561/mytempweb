using CRM.Entity.Model.Common;

namespace CRM.Entity.Model.Customer
{
    public class PersonDocumentDto : BaseDto
    {
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public int DocumentId { get; set; }
        public DocumentDto Document { get; set; }
        public string Comment { get; set; }
        public string DocumentTypeName { get; set; }
        public byte[] ImageThumbnailContent { get; set; }
        public bool IsAntiSocialBehaviour { get; set; }
    }
}

using CRM.Dto.Common;

namespace CRM.Dto.Employee
{
    public class UserDocumentDto : BaseDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int DocumentId { get; set; }
        public DocumentDto Document { get; set; }
        public string Comment { get; set; }
    }
}

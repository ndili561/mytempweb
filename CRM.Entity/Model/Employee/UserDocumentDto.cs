using CRM.Entity.Model.Common;

namespace CRM.Entity.Model.Employee
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

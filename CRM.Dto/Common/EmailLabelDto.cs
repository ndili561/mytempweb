using CRM.Dto.Lookup;

namespace CRM.Dto.Common
{
    public class EmailLabelDto : BaseDto
    {
        public int EmailId { get; set; }
        public virtual EmailDto Email { get; set; }

        public int EmailLabelTypeId { get; set; }
        public virtual EmailLabelTypeDto EmailLabelType { get; set; }

    }
}

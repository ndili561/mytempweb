using System.Collections.Generic;

namespace CRM.Dto.Common
{
    public class PropertyVoidDto : BaseDto
    {
        public int PropertyId { get; set; }
        public PropertyDto Property { get; set; }
        public int VoidId { get; set; }
        public List<PropertyVoidViewDto> PropertyVoids { get; set; }

    }
}

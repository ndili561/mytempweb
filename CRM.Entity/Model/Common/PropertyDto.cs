using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Model.Common
{
    public class PropertyDto : BaseDto
    {
        public PropertyDto()
        {
            Tenants = new List<TenantDto>();
            PropertyDocuments = new List<PropertyDocumentDto>();
            PropertyVoids = new List<PropertyVoidDto>();
            PropertyUserTasks = new List<PropertyUserTaskDto>();
        }
        public string PropertyCode { get; set; }
        [NotMapped]
        public PropertyDetailViewDto PropertyDetailView { get; set; }
        public List<TenantDto> Tenants { get; set; }
        public List<PropertyVoidDto> PropertyVoids { get; set; }
        public List<PropertyDocumentDto> PropertyDocuments { get; set; }
        public List<PropertyUserTaskDto> PropertyUserTasks { get; set; }
    }
}

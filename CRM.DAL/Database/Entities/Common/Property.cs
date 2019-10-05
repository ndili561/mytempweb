using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Customer;

namespace CRM.DAL.Database.Entities.Common
{
    public class Property : BaseEntity
    {
        public Property()
        {
            Tenants = new List<Tenant>();
            PropertyVoids = new List<PropertyVoid>();
        }
        public string PropertyCode { get; set; }
        [NotMapped]
        public PropertyDetailView PropertyDetailView { get; set; }
        public List<Tenant> Tenants { get; set; }
        public List<PropertyVoid> PropertyVoids { get; set; }
        public List<PropertyDocument> PropertyDocuments { get; set; }
        public List<PropertyUserTask> PropertyUserTasks { get; set; }
    }
}

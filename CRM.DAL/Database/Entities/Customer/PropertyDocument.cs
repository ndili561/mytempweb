using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Customer
{
    public class PropertyDocument : AuditBaseEntity
    {
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
        public int DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        public string Comment { get; set; }
      
        public int ViewOrder { get; set; }
        public bool IsDefaultImage { get; set; }
        public bool IsImageForPropertyShop { get; set; }
        public int? ImageGroupId { get; set; }
        [ForeignKey("ImageGroupId")]
        public ImageGroup ImageGroup { get; set; }
    }
}

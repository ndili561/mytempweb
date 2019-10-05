using CRM.Entity.Model.Lookup;

namespace CRM.Entity.Model.Common
{
    public class PropertyDocumentDto : BaseDto
    {
        public int PropertyId { get; set; }
        public PropertyDto Property { get; set; }
        public int DocumentId { get; set; }
        public DocumentDto Document { get; set; }
        public string Comment { get; set; }
        public string DocumentTypeName { get; set; }
        public byte[] ImageThumbnailContent { get; set; }
        public bool IsDefaultImage { get; set; }
        public bool IsImageForPropertyShop { get; set; }
        public int ViewOrder { get; set; }
        public int? ImageGroupId { get; set; }
        public ImageGroupDto ImageGroup { get; set; }
    }
}

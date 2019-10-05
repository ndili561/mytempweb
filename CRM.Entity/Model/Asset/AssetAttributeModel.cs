using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;

namespace CRM.Entity.Model.Asset
{
    public class AssetAttributeModel : BaseModel
    {
        public int AssetId { get; set; }
        [Display(Name = "Asset")]
        public virtual AssetModel Asset { get; set; }
        [Display(Name = "Attribute")]
        public int AttributeId { get; set; }
        [Display(Name = "Attribute")]
        public virtual AttributeModel Attribute { get; set; }
        [MaxLength(500)]
        [Display(Name = "Comment")]
        public virtual string Comment { get; set; }
        public virtual string AssetAttributeValue { get; set; }


        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
        [Display(Name = "Inculde In Survey")]
        [DefaultValue(true)]
        public bool IncludeInSurvey { get; set; }
    }
}

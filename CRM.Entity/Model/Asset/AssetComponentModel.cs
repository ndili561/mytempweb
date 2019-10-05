using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;

namespace CRM.Entity.Model.Asset
{
    public class AssetComponentModel : BaseModel
    {
        public AssetComponentModel()
        {
            AssetComponentComponentElementAttributes = new List<AssetComponentComponentElementAttributeModel>();
        }

        [Display(Name = "Component")]
        public int? ComponentElementId { get; set; }
        public ComponentElementModel ComponentElement { get; set; }
        [Display(Name = "Asset")]
        public int AssetId { get; set; }
        public virtual AssetModel Asset { get; set; }
        public virtual string Comment { get; set; }
        public virtual bool Active { get; set; }
        public List<AssetComponentComponentElementAttributeModel> AssetComponentComponentElementAttributes { get; set; }
        
 
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
        [DefaultValue(true)]
        public bool IncludeInSurvey { get; set; }
        public virtual bool IsMainComponent { get; set; }

    }
}
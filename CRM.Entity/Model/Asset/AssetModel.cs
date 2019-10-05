using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;

namespace CRM.Entity.Model.Asset
{
    public class AssetModel:BaseModel

    {
        public AssetModel()
        {
            AssetComponents = new List<AssetComponentModel>();
            AssetAttributes = new List<AssetAttributeModel>();
        }

        [Display(Name = "Property Code")]
        public string PropertyCode { get; set; }
        [Display(Name = "Asset Id")]
        public int AssetId { get; set; }
      
        [Display(Name = "Parent Asset Id")]
        public int? ParentAssetId { get; set; }
        public virtual AssetModel ParentAsset { get; set; }
        public List<AssetComponentModel> AssetComponents { get; set; }
        public List<AssetAttributeModel> AssetAttributes { get; set; }

        [Display(Name = "Asset Type")]
        public string AssetType { get; set; }
        public AssetTenantModel AssetTenantModel { get; set; }
       
    }
}

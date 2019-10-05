using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Asset
{
   
    public class AssetComponentComponentElementAttributeModel : BaseModel
    {
        public  AssetComponentComponentElementAttributeModel()
        {
            ComponentElementAttributeValueSelectList = new List<SelectListItem>();
        }
        public int AssetComponentId { get; set; }
        public virtual AssetComponentModel AssetComponent { get; set; }
        public int ComponentElementAttributeId { get; set; }
        public virtual ComponentElementAttributeModel ComponentElementAttribute { get; set; }
        [MaxLength(500)]
        public virtual string Comment { get; set; }
        public virtual string ComponentElementAttributeValue { get; set; }
        public virtual List<SelectListItem> ComponentElementAttributeValueSelectList { get; set; }
        public virtual bool HasAudit { get; set; }
        [Display(Name = "Inculde In Survey")]
        [DefaultValue(true)]
        public bool IncludeInSurvey { get; set; }

        public bool Active { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Asset
{
    public class ComponentElementModel :BaseTypeModel
    {
        [Display(Name= "Component Group")]
        public int? ComponentId { get; set; }
        public virtual ComponentGroupModel ComponentGroup { get; set; }
        public virtual List<AssetComponentModel> AssetComponents { get; set; }
        public List<ComponentElementAttributeModel> ComponentElementAttributes { get; set; }
        public List<SelectListItem> ComponentGroupsSelectList { get; set; }
        public int? ParentComponentId { get; set; }
        public virtual ComponentElementModel ParentComponent { get; set; }
        public int? ComponentTypeId { get; set; }
        public virtual ComponentTypeModel ComponentType { get; set; }
        [Display(Name = "Is Default For All Assets")]
        public bool IsDefaultForAllAssets { get; set; }

        [Display(Name = "Default Value")]
        public string DefaultValue { get; set; }
        [Display(Name = "Inculde In Survey")]
        public bool IncludeInSurvey { get; set; }

    }
}
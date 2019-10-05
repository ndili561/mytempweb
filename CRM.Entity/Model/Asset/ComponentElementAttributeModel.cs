using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Asset
{
    public class ComponentElementAttributeModel : BaseModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "ComponentId")]
        public int ComponentElementId { get; set; }
        public virtual ComponentElementModel ComponentElement { get; set; }
        [Display(Name = "AttributeId")]
        public int AttributeId { get; set; }
        public virtual AttributeModel Attribute { get; set; }

        public List<SelectListItem> AttributeSelectList { get; set; }
        [MaxLength(500)]
        [Display(Name = "Comment")]
        public virtual string Comment { get; set; }
        [Display(Name = "Inculde In Survey")]
        public bool IncludeInSurvey { get; set; }
        [Display(Name = "Is Default")]
        public bool IsDefault { get; set; }
    }
}

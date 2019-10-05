using System.Collections.Generic;

namespace CRM.Entity.Model.Asset
{
    public class ComponentGroupModel:BaseTypeModel
    {
        public virtual List<ComponentElementModel> ComponentElements { get; set; }
    }
}
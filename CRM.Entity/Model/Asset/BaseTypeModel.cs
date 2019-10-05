using System.ComponentModel.DataAnnotations;
using Core.Utilities;

namespace CRM.Entity.Model.Asset
{
    public interface IBaseTypeModel
    {
         int Id { get; set; }
         string Code { get; set; }
         string Name { get; set; }
         string Description { get; set; }
         bool Active { get; set; }
    }
    public class BaseTypeModel: BaseModel,IBaseTypeModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
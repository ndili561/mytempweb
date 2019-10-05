using System.ComponentModel.DataAnnotations;

namespace CRM.Entity.Model.Customer
{
    public class ApplicationStatusDto
    {
        public int ApplicationStatusID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public int? SortOrder { get; set; }

        public bool StatusIsOpen { get; set; }
    }
}

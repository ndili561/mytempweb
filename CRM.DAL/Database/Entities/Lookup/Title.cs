using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Lookup
{
    public class Title : BaseLookup
    {
        public int? DefaultGenderId { get; set; }
        [ForeignKey("DefaultGenderId")]
        public Gender Gender { get; set; }

    }
}

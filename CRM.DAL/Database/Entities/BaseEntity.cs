using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using CRM.DAL.Database.Entities.Employee;

namespace CRM.DAL.Database.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public List<Audit> Audits { get; set; }
        //Used to check concurrency
        [ConcurrencyCheck, Timestamp]
        public virtual byte[] RowVersion { get; set; }
       
        [NotMapped]
        public virtual string ErrorMessage { get; set; }
        [NotMapped]
        public virtual string SuccessMessage { get; set; }

        [NotMapped]
        //Incase client want to send the user details in object
        public IPrincipal CurrentUser { get; set; }

     
    }
}

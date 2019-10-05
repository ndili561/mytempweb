using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Core.Utilities
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        //Used to check concurrency
        [ConcurrencyCheck, Timestamp]
        public virtual byte[] RowVersion { get; set; }
        public string LastModifiedBy { get; set; }
        public Guid? LastModifiedById { get; set; }

        public virtual DateTime? LastModifiedOn { get; set; }

        public virtual string ErrorMessage { get; set; }
        public virtual string SuccessMessage { get; set; }

        //Incase client want to send the user details in object
        public IPrincipal CurrentUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
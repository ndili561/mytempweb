using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace CRM.Entity
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateTime LastModifiedOn { get; set; }

        //[Required] // Not required as the Id is passed in request during update who is doing it
        public Guid? LastModifiedById { get; set; }
        public byte[] RowVersion { get; set; }
        [MaxLength(150)]
        public virtual string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public virtual string ErrorMessage { get; set; }
        public virtual string SuccessMessage { get; set; }

        //Incase client want to send the user details in object
        public IPrincipal CurrentUser { get; set; }

    }
}
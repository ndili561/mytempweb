using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Database.Entities.Employee
{
    public class IdentityUserView
    {

        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PersonId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Roles { get; set; }
    }
}

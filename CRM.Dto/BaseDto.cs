using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using CRM.Dto.Employee;

namespace CRM.Dto
{
    public class BaseDto
    {
        public int Id { get; set; }
        public List<AuditDto> Audits { get; set; }
        public virtual byte[] RowVersion { get; set; }

        public virtual string ErrorMessage { get; set; }
        public virtual string SuccessMessage { get; set; }

        public IPrincipal CurrentUser { get; set; }
    }
}
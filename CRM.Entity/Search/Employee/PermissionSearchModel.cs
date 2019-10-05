using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class PermissionSearchModel : BaseFilterModel
    {
        public PermissionSearchModel()
        {
           PermissionSearchResult = new List<PermissionDto>();
        }
        public List<PermissionDto> PermissionSearchResult { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
    }
}
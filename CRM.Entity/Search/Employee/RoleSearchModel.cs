using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class RoleSearchModel : BaseFilterModel
    {
        public RoleSearchModel()
        {
           RoleSearchResult = new List<RoleDto>();
        }

        public int  Id { get; set; }    
        public List<RoleDto> RoleSearchResult { get; set; }
        public int ApplicationId { get; set; }
    }
}
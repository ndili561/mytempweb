using System.Collections.Generic;
using Core.Utilities;
using CRM.Entity.Model.Employee;

namespace CRM.Entity.Search.Employee
{
    public class MenuItemSearchModel : BaseFilterModel
    {
        public MenuItemSearchModel()
        {
           MenuItemSearchResult = new List<MenuItemDto>();
        }
        public List<MenuItemDto> MenuItemSearchResult { get; set; }
        public string Name { get; set; }
        public int ApplicationId { get; set; }
    }
}
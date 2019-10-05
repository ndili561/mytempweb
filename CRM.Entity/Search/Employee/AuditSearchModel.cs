using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Search.Employee
{
    public class AuditSearchModel : BaseFilterModel
    {
        public AuditSearchModel()
        {
            AuditSearchResult = new List<AuditModel>();
            TableList= new List<string>();
        }
        public List<AuditModel> AuditSearchResult { get; set; }
        public int Id { get; set; }
        [DisplayName("Table Name")]
        public string TableName { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Display(Name = "Table")]
        public string SelectedTable { get; set; }
        public List<string> TableList { get; set; }
        public List<SelectListItem> TableSelectList {
            get
            {
                return TableList?.Select(table => new SelectListItem {Value = table, Text = table}).ToList() ?? new List<SelectListItem>();
            }
        }

        public int PersonId { get; set; }
        public int AddressId { get; set; }
    }
}
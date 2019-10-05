using System.Collections.Generic;
using System.ComponentModel;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Common
{
   public class CalendarTask
    {
        
        public string Events { get; set; }
        public string CalendarViewType { get; set; }
        public string CalendarStartDate { get; set; }

        public string CalendarEndDate { get; set; }
        public int EmployeeId { get; set; }
    }
}

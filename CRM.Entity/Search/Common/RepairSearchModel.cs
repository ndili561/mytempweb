using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class RepairSearchModel : BaseFilterModel
    {
        public RepairSearchModel()
        {
            RepairSearchResult = new List<RepairDto>();
        }

        public List<RepairDto> RepairSearchResult { get; set; }

        [Display(Name = "Reapir Id")]
        public int RepairId { get; set; }
        [Display(Name = "Reapir Line Id")]
        public int RepairLineId { get; set; }
        [Display(Name = "Visit Id")]
        public int VisitiId { get; set; }

        [DisplayName("Property Reference")]
        public string PropertyReference { get; set; }
        [DisplayName("Operative Name")]
        public string OperativeName { get; set; }
        [DisplayName("Arrival Date")]
        public DateTime? ArrivalDate { get; set; }

        public string OperativeCode { get; set; }
        public string VisitStatus { get; set; }
        public string RepairTrade { get; set; }
    }
}
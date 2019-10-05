using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;
using CRM.Entity.Model.Common;

namespace CRM.Entity.Search.Common
{
    public class VanLocationSearchModel : BaseFilterModel
    {
        public VanLocationSearchModel()
        {
            VanLocationSearchResult = new List<VanLocationDto>();
        }

        public List<VanLocationDto> VanLocationSearchResult { get; set; }
        [Key]
        public string RegistrationNumber { get; set; }
        
        public string Name { get; set; }
      
        public double Longitude { get; set; }
       
        public double Latitude { get; set; }

        public DateTime ReceivedTime { get; set; }
        public string EmployeeRef { get; set; }
    }
}

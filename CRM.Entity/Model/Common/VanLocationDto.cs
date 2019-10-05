using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entity.Model.Common
{
    public class VanLocationDto
    {
        [Key]
        public string RegistrationNumber { get; set; }
        public string EmployeeRef { get; set; }

        public string Name { get; set; }
      
        public double Longitude { get; set; }
       
        public double Latitude { get; set; }

        public DateTime ReceivedTime { get; set; }
         
    }
}

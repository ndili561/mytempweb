using System;
using System.Collections.Generic;
using CRM.Dto.Lookup;

namespace CRM.Dto.Customer
{
    public class PersonAlertDto : BaseDto
    {
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public string Details { get; set; }
        public DateTime RaisedOn { get; set; }
        public int? AlertStatusId { get; set; }
        public AlertStatusDto AlertStatus { get; set; }
        public int? AlertTypeId { get; set; }
        public AlertTypeDto AlertType { get; set; }
        public List<PersonAlertCommentDto> PersonAlertComments { get; set; }
    }
}

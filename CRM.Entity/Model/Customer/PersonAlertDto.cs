using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Customer
{
    public class PersonAlertDto : BaseDto
    {
        public PersonAlertDto()
        {
            AlertGroupSelectList =new List<SelectListItem>();
            AlertTypeSelectList  = new List<SelectListItem>();
            AlertStatusSelectList  = new List<SelectListItem>();
            PersonAlertComments = new List<PersonAlertCommentDto>();
        }
        public int PersonId { get; set; }
       
        public PersonDto Person { get; set; }
        public string Details { get; set; }
        [Display(Name = "Raised On")]
        public DateTime RaisedOn { get; set; }

        public string RaisedOnString => RaisedOn.ToShortDateString();
        [Display(Name = "Alert Status")]
        public int? AlertStatusId { get; set; }
        public List<SelectListItem> AlertStatusSelectList { get; set; }
        public AlertStatusDto AlertStatus { get; set; }
        [Display(Name = "Alert Type")]
        public int? AlertTypeId { get; set; }
        public List<SelectListItem> AlertTypeSelectList { get; set; }
        public AlertTypeDto AlertType { get; set; }
        [Display(Name = "Alert Group")]
        public int? AlertGroupId { get; set; }
        public List<SelectListItem> AlertGroupSelectList { get; set; }
        public AlertGroupDto AlertGroup { get; set; }

        public List<PersonAlertCommentDto> PersonAlertComments { get; set; }
    }
}

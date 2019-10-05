using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Customer
{
    public class PersonCaseDto: BaseDto
    {
        public PersonCaseDto()
        {
            CaseStatusSelectList = new List<SelectListItem>();
            CaseTypeSelectList = new List<SelectListItem>();
            PrioritySelectList = new List<SelectListItem>();
        }
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public string Details { get; set; }
        [Display(Name = "Raised On")]
        public DateTime RaisedOn { get; set; }
        public string RaisedOnString => RaisedOn.ToShortDateString();
        public int UserId { get; set; }
        public UserDto CreateBy { get; set; }
        [Display(Name = "Case Status")]
        public int? CaseStatusId { get; set; }
        public PersonCaseStatusDto CaseStatus { get; set; }
        public List<SelectListItem> CaseStatusSelectList { get; set; }
        [Display(Name = "Case Type")]
        public int? CaseTypeId { get; set; }
        public PersonCaseTypeDto CaseType { get; set; }
        public List<SelectListItem> CaseTypeSelectList { get; set; }
        [Display(Name = "Priority")]
        public int? PriorityId { get; set; }
        public PriorityDto Priority { get; set; }
        public List<SelectListItem> PrioritySelectList { get; set; }
    }
}

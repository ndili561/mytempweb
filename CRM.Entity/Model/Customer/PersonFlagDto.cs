using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Entity.Model.Lookup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Customer
{
    public class PersonFlagDto : BaseDto
    {
        public PersonFlagDto()
        {
            FlagGroupSelectList = new List<SelectListItem>();
            FlagTypeSelectList = new List<SelectListItem>();
            PersonFlagComments = new List<PersonFlagCommentDto>();
        }
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public string Details { get; set; }
        [Display(Name = "Raised On")]
        public DateTime RaisedOn { get; set; }
        public string RaisedOnString => RaisedOn.ToShortDateString();
        [Display(Name = "Flag Group")]
        public int? FlagGroupId { get; set; }
        public List<SelectListItem> FlagGroupSelectList { get; set; }
        public FlagGroupDto FlagGroup { get; set; }
        [Display(Name = "Flag Type")]
        public int? FlagTypeId { get; set; }
        public FlagTypeDto FlagType { get; set; }
        public List<SelectListItem> FlagTypeSelectList { get; set; }
        public bool IsActive { get; set; }
        public List<PersonFlagCommentDto> PersonFlagComments { get; set; }
    }
}

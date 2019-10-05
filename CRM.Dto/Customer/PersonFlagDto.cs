using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.Dto.Lookup;

namespace CRM.Dto.Customer
{
    public class PersonFlagDto : BaseDto
    {
        public PersonFlagDto()
        {
            PersonFlagComments = new List<PersonFlagCommentDto>();
        }
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public string Details { get; set; }
        public DateTime DateTime { get; set; }
        [Display(Name = "Flag Group")]
        public int? FlagGroupId { get; set; }
        public FlagGroupDto FlagGroup { get; set; }
        [Display(Name = "Flag Type")]
        public int? FlagTypeId { get; set; }
        public FlagTypeDto FlagType { get; set; }
        public bool IsActive { get; set; }
        public List<PersonFlagCommentDto> PersonFlagComments { get; set; }
    }
}

using System;
using CRM.Dto.Employee;
using CRM.Dto.Lookup;

namespace CRM.Dto.Customer
{
    public class PersonCaseDto: BaseDto
    {
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public string Details { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public UserDto CreateBy { get; set; }
        public int? CaseStatusId { get; set; }
        public PersonCaseStatusDto CaseStatus { get; set; }
        public int? CaseTypeId { get; set; }
        public PersonCaseTypeDto CaseType { get; set; }
    }
}

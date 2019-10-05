using System;
using System.Collections.Generic;
using System.ComponentModel;
using Core.Utilities;
using CRM.Entity.Model.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Search.Customer
{
    public class PersonSearchModel : BaseFilterModel
    {
        public PersonSearchModel()
        {
            PersonSearchResult = new List<PersonDto>();
            GenderList= new List<SelectListItem>();
            EthnicityList = new List<SelectListItem>();
            NationalityTypeList = new List<SelectListItem>();
            PreferredLanguageList = new List<SelectListItem>();
        }
        public List<PersonDto> PersonSearchResult { get; set; }
        public string ErrorMessage { get; set; }

        public string Forename { get; set; }
        public string Surname { get; set; }
        [DisplayName("Gender")]
        public int? GenderId { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        [DisplayName("Ethnicity")]
        public int? EthnicityId { get; set; }
        public List<SelectListItem> EthnicityList { get; set; }
        [DisplayName("Nationality")]
        public int? NationalityTypeId { get; set; }
        public List<SelectListItem> NationalityTypeList { get; set; }

        [DisplayName("Preferred Language")]
        public int? PreferredLanguageId { get; set; }
        public List<SelectListItem> PreferredLanguageList { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Postcode")]
        public string Postcode { get; set; }
        [DisplayName("Date of birth")]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Telephone")]
        public string TelephoneNumber { get; set; }
        [DisplayName("Mobile")]
        public string MobileNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("NI Number")]
        public string NationalInsuranceNumber { get; set; }
        [DisplayName("Application Id")]
        public int ApplicationId { get; set; }

        public int MainContactPersonId { get; set; }
        public int PersonId { get; set; }
    }
}
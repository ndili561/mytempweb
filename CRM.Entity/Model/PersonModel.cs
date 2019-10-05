using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model
{
    public class PersonModel : BaseModel
    {
        public PersonModel()
        {
            GenderSelectList = new List<SelectListItem>();
            EthnicitySelectList = new List<SelectListItem>();
            NationalitySelectList = new List<SelectListItem>();
            LanguageSelectList = new List<SelectListItem>();
            TitleSelectList = new List<SelectListItem>();
        }
        [Display(Name = "NI No.")]
        public string NationalInsuranceNumber { get; set; }
        [Display(Name = "Title")]
        public int? TitleId { get; set; }
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        [Display(Name = "Gender")]
        public int? GenderId { get; set; }
        public string Gender { get; set; }
        public List<SelectListItem> GenderSelectList { get; set; }
        [Display(Name = "Ethnicity")]
        public int? EthnicityId { get; set; }
        public string Ethnicity { get; set; }
        public IEnumerable<SelectListItem> EthnicitySelectList { get; set; }
        [Display(Name = "Nationality")]
        public int? NationalityTypeId { get; set; }
        public string Nationality { get; set; }
        public List<SelectListItem> NationalitySelectList { get; set; }
        [Display(Name = "Language")]
        public int? PreferredLanguageId { get; set; }
        public string Language { get; set; }
        public List<SelectListItem> LanguageSelectList { get; set; }
        public List<SelectListItem> TitleSelectList { get; set; }
        public override string ToString()
        {
            return Forename + " " + Surname;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CRM.Entity.Helper;
using CRM.Entity.Model.Customer;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Customer;
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Facade;

namespace CRM.Web.Controllers
{
    public class TestController : BaseController
    {
        private readonly IGatewayFacadeApiClient _facadeApiClient;
        private readonly ICustomerFacadeApiClient _personApiClient;
        public TestController(IGatewayFacadeApiClient facadeApiClient, ICustomerFacadeApiClient personApiClient)
        {
            _facadeApiClient = facadeApiClient;
            _personApiClient = personApiClient;
        }
        [HttpGet]
        public IActionResult Titles()
        {
            //var lookup = _facadeApiClient.GetLookup().Result;
            try
            {
                var result = _facadeApiClient.GetLookupUsingOdata(new List<string> { nameof(LookupDto.Genders), nameof(LookupDto.Titles), nameof(LookupDto.Nationalities) }).Result;
                var model = result.value.FirstOrDefault();
                var person = _personApiClient.GetPerson(1).Result;
                return View("Views/Home/TestGet.cshtml", model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
           
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = InitializeModel(null);
            return View(model);
        }
        private PersonSearchModel InitializeModel(PersonSearchModel model)
        {
            model = InitializeSearchModel(model, "PersonGrid", "Id", "Desc");
            return model;
        }
        [HttpGet]
        public async Task<IActionResult> Grid(PersonSearchModel model)
        {
            try
            {
                model = InitializeModel(model);
                var result = await _personApiClient.GetPersons(model);
                PopulateValuesFromLookup(result);
                return PartialView(result);
            }
            catch (Exception e)
            {
                var sb = new StringBuilder();
                while (e!=null)
                {
                    sb.AppendLine(e.Message);
                    e = e.InnerException;
                }
                return PartialView(new PersonSearchModel{ErrorMessage = sb.ToString()});
            }
           
        }
        private void PopulateValuesFromLookup(PersonSearchModel result)
        {
            var lookups = _facadeApiClient.GetLookup().Result;
            foreach (var personDto in result.PersonSearchResult)
            {
                UpdateCustomProperty(personDto, lookups);
            }
        }
        
        private static void UpdateCustomProperty(PersonDto personDto, LookupDto lookups)
        {
            var gender = lookups.Genders.FirstOrDefault(x => x.Id == personDto.GenderId);
            if (gender != null)
                personDto.Gender = gender.Name;
            var ethnicity = lookups.Ethnicities.FirstOrDefault(x => x.Id == personDto.EthnicityId);
            if (ethnicity != null)
                personDto.Ethnicity = ethnicity.Name;
            var language = lookups.Languages.FirstOrDefault(x => x.Id == personDto.PreferredLanguageId);
            if (language != null)
                personDto.Language = language.Name;
            var title = lookups.Titles.FirstOrDefault(x => x.Id == personDto.TitleId);
            if (title != null)
                personDto.Title = title.Name;
            var nationality = lookups.Nationalities.FirstOrDefault(x => x.Id == personDto.NationalityTypeId);
            if (nationality != null)
                personDto.Nationality = nationality.Name;
        }

        [HttpGet]
        public IActionResult Details(int personId)
        {
            var person = GetPersonForDetails(personId);
            ViewData["Title"] = person.Title + " " + person.Forename + " " + person.Surname;
            if (string.IsNullOrWhiteSpace(person.TenantCode))
            {
                person.Tenant = _personApiClient.GetTenantByTenantCode(person.TenantCode).Result;
              // person.Tenant.Property.PropertyDetailView = _propertyFacadeApiClient.GetPropertyDetailView(person.Tenant.Property.PropertyCode).Result;
            }
            if (person.ApplicationId.HasValue)
            {
                person.VblApplication = _personApiClient.GetVblApplication(person.ApplicationId.Value).Result;
            }
            if (person.MainContactPersonId.HasValue)
            {
                var householdMembers = _personApiClient.GetPersonByMainContactId(person.MainContactPersonId.Value).Result?.Where(x => x.Id != x.MainContactPersonId).ToList();
                person.HouseholdMembers.AddRange(householdMembers);
            }
            return View(person);
        }
        private PersonDto GetPersonForDetails(int personId)
        {
            var person = _personApiClient.GetPerson(personId).Result;
            GetPersonForContactDetails(person);
            return person;
        }
        private void GetPersonForContactDetails(PersonDto model)
        {
            PopulateDropdownList(model);
            var lookups = _facadeApiClient.GetLookup().Result;
            UpdateCustomProperty(model, lookups);
        }
        private void PopulateDropdownList(PersonDto person)
        {
            if (person == null)
            {
                return;
            }
            var lookups = _facadeApiClient.GetLookup().Result;
            var titles = lookups.Titles?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var ethinicities = lookups.Ethnicities?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var genders = lookups.Genders?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var languages = lookups.Languages?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var relationships = lookups.Relationships?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);
            var nationalities = lookups.Nationalities?.Where(x => x.IsActive).ToList().ConvertAll(x => (BaseLookupDto)x);

            person.TitleSelectList = SelectedListHelper.GetSelectListForItems(titles, person.TitleId?.ToString());
            person.GenderSelectList = SelectedListHelper.GetSelectListForItems(genders, person.GenderId?.ToString());
            person.EthnicitySelectList = SelectedListHelper.GetSelectListForItems(ethinicities, person.EthnicityId?.ToString());
            person.NationalitySelectList = SelectedListHelper.GetSelectListForItems(nationalities, person.NationalityTypeId?.ToString());
            person.LanguageSelectList = SelectedListHelper.GetSelectListForItems(languages, person.PreferredLanguageId?.ToString());
            person.RelationshipSelectList = SelectedListHelper.GetSelectListForItems(relationships, person.RelationshipId?.ToString());
            GetContactDetails(person, lookups);
        }
        private void GetContactDetails(PersonDto contact, LookupDto lookUp)
        {
            var persistedContactDetailIds = contact.PersonContactDetails.Select(x => x.ContactByOptionId).ToList();
            contact.ContactByOptions = lookUp.ContactByOptions.OrderBy(x => x.SortOrder).ToList();
            foreach (var contactByOption in lookUp.ContactByOptions.Where(x => !persistedContactDetailIds.Contains(x.Id)).OrderBy(x => x.SortOrder).ToList())
            {
                contact.PersonContactDetails.Add(new PersonContactDetailDto { ContactByOptionId = contactByOption.Id, ContactByOption = contactByOption });
            }
            foreach (var contactDetail in contact.PersonContactDetails)
            {
                contactDetail.ContactByOption = lookUp.ContactByOptions.FirstOrDefault(x => x.Id == contactDetail.ContactByOptionId);
                if (contactDetail.Id > 0)
                {
                    contact.PersonContactDetails.First(x => x.ContactByOptionId == contactDetail.ContactByOptionId).ContactValue = contactDetail.ContactValue;
                    contact.PersonContactDetails.First(x => x.ContactByOptionId == contactDetail.ContactByOptionId).Comment = contactDetail.Comment;
                    contact.PersonContactDetails.First(x => x.ContactByOptionId == contactDetail.ContactByOptionId).IsSelected = true;
                    contact.PersonContactDetails.First(x => x.ContactByOptionId == contactDetail.ContactByOptionId).ContactByOption = lookUp.ContactByOptions.FirstOrDefault(x => x.Id == contactDetail.ContactByOptionId);
                }

            }
        }
    }
}
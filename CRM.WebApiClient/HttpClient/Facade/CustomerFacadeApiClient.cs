using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;
using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Employee;
using CRM.WebApiClient.Interface.Facade;

namespace CRM.WebApiClient.HttpClient.Facade
{
    public class CustomerFacadeApiClient: ICustomerFacadeApiClient
    {
        private readonly IAddressApiClient _addressApiClient;
        private readonly IAuditApiClient _auditApiClient;
        private readonly IPersonAddressApiClient _personAddressApiClient;
        private readonly IPersonApiClient _personApiClient;
        private readonly IPersonApplicationLinkApiClient _personApplicationLinkApiClient;
        private readonly IPersonDocumentApiClient _personDocumentApiClient;
        private readonly IPersonEmailApiClient _personEmailApiClient;
        private readonly IPersonSmsApiClient _personSmsApiClient;
        private readonly IPersonAlertApiClient _personAlertApiClient;
        private readonly IPersonAlertCommentApiClient _personAlertCommentApiClient;
        private readonly IPersonAntiSocialBehaviourApiClient _personAntiSocialBehaviourApiClient;
        private readonly IPersonAntiSocialBehaviourCaseNoteApiClient _personAntiSocialBehaviourCaseNoteApiClient;
        private readonly IPersonFlagApiClient _personFlagApiClient;
        private readonly IPersonFlagCommentApiClient _personFlagCommentApiClient;
        private readonly IPersonCaseApiClient _personCaseApiClient;
        private readonly ITenantApiClient _tenantApiClient;
        private readonly ITenantHistoryViewApiClient _tenantHistoryViewApiClient;
        private readonly IVblApplicationApiClient _vblApplicationApiClient;

        public CustomerFacadeApiClient(IAddressApiClient addressApiClient,
            IAuditApiClient auditApiClient,
            IPersonAddressApiClient personAddressApiClient,
            IPersonAlertApiClient personAlertApiClient,
            IPersonAlertCommentApiClient personAlertCommentApiClient,
            IPersonFlagApiClient personFlagApiClient,
            IPersonFlagCommentApiClient personFlagCommentApiClient,
            IPersonCaseApiClient personCaseApiClient,
            IPersonApiClient personApiClient, 
            IPersonApplicationLinkApiClient personApplicationLinkApiClient,
            IPersonAntiSocialBehaviourApiClient personAntiSocialBehaviourApiClient,
            IPersonAntiSocialBehaviourCaseNoteApiClient personAntiSocialBehaviourCaseNoteApiClient,
            IPersonDocumentApiClient personDocumentApiClient,
            IPersonEmailApiClient personEmailApiClient,
            IPersonSmsApiClient personSmsApiClient,
            ITenantApiClient tenantApiClient,
            ITenantHistoryViewApiClient tenantHistoryViewApiClient,
            IVblApplicationApiClient vblApplicationApiClient
            )
        {
            _addressApiClient = addressApiClient;
            _auditApiClient = auditApiClient;
            _personAddressApiClient = personAddressApiClient;
            _personAlertApiClient = personAlertApiClient;
            _personAlertCommentApiClient = personAlertCommentApiClient;
            _personFlagApiClient = personFlagApiClient;
            _personFlagCommentApiClient = personFlagCommentApiClient;
            _personCaseApiClient = personCaseApiClient;
            _personApiClient = personApiClient;
            _personApplicationLinkApiClient = personApplicationLinkApiClient;
            _personAntiSocialBehaviourApiClient = personAntiSocialBehaviourApiClient;
            _personAntiSocialBehaviourCaseNoteApiClient = personAntiSocialBehaviourCaseNoteApiClient;
            _personDocumentApiClient = personDocumentApiClient;
            _personSmsApiClient = personSmsApiClient;
            _personEmailApiClient = personEmailApiClient;
            _tenantApiClient = tenantApiClient;
            _tenantHistoryViewApiClient = tenantHistoryViewApiClient;
            _vblApplicationApiClient = vblApplicationApiClient;
        }

        public async Task<AddressSearchModel> GetAddresses(AddressSearchModel model)
        {
           return await _addressApiClient.GetAddresses(model);
        }

        public async Task<AddressDto> GetAddress(int addressId)
        {
            return await _addressApiClient.GetAddress(addressId);
        }

        public async Task<AddressDto> PostAddress(AddressDto model)
        {
            return await _addressApiClient.PostAddress(model);
        }

        public async Task<AddressDto> PutAddress(int id, AddressDto model)
        {
            return await _addressApiClient.PutAddress(id,model);
        }

        public async Task<PersonAddressSearchModel> GetPersonAddresses(PersonAddressSearchModel model)
        {
            return await _personAddressApiClient.GetPersonAddresses(model);
        }

        public async Task<PersonAddressDto> GetPersonAddress(int personAddressId)
        {
            return await _personAddressApiClient.GetPersonAddress(personAddressId);
        }

        public async Task<PersonAddressDto> PostPersonAddress(PersonAddressDto model)
        {
            return await _personAddressApiClient.PostPersonAddress(model);
        }

        public async Task<PersonAddressDto> PutPersonAddress(int id, PersonAddressDto model)
        {
            return await _personAddressApiClient.PutPersonAddress(id,model);
        }

        public async Task<PersonSearchModel> GetPersons(PersonSearchModel model)
        {
            return await _personApiClient.GetPersons(model);
        }

        public async Task<PersonDto> GetPerson(string globalIdentityKey)
        {
            return await _personApiClient.GetPerson(globalIdentityKey);
        }

        public async Task<PersonAlertSearchModel> GetPersonAlerts(PersonAlertSearchModel model)
        {
            return await _personAlertApiClient.GetPersonAlerts(model);
        }

        public async Task<PersonAlertDto> GetPersonAlert(int personAlertId)
        {
            return await _personAlertApiClient.GetPersonAlert(personAlertId);
        }

        public async Task<PersonDto> GetPerson(int personId)
        {
            return await _personApiClient.GetPerson(personId);
        }

        public async Task<PersonAntiSocialBehaviourSearchModel> GetPersonAntiSocialBehaviours(PersonAntiSocialBehaviourSearchModel model)
        {
            return await _personAntiSocialBehaviourApiClient.GetPersonAntiSocialBehaviours(model);
        }

        public async Task<PersonAntiSocialBehaviourDto> GetPersonAntiSocialBehaviour(int id)
        {
            return await _personAntiSocialBehaviourApiClient.GetPersonAntiSocialBehaviour(id);
        }

        public async Task<PersonAntiSocialBehaviourDto> PostPersonAntiSocialBehaviour(PersonAntiSocialBehaviourDto model)
        {
            return await _personAntiSocialBehaviourApiClient.PostPersonAntiSocialBehaviour(model);
        }

        public async Task<PersonAntiSocialBehaviourDto> PutPersonAntiSocialBehaviour(int id, PersonAntiSocialBehaviourDto model)
        {
            return await _personAntiSocialBehaviourApiClient.PutPersonAntiSocialBehaviour(id,model);
        }

      

        public async Task<HttpResponseMessage> DeletePersonAntiSocialBehaviour(int id)
        {
            return await _personAntiSocialBehaviourApiClient.DeletePersonAntiSocialBehaviour(id);
        }

        public async Task<PersonAntiSocialBehaviourCaseNoteSearchModel> GetPersonAntiSocialBehaviourCaseNotes(PersonAntiSocialBehaviourCaseNoteSearchModel model)
        {
            return await _personAntiSocialBehaviourCaseNoteApiClient.GetPersonAntiSocialBehaviourCaseNotes(model);
        }

        public async Task<PersonAntiSocialBehaviourCaseNoteDto> GetPersonAntiSocialBehaviourCaseNote(int id)
        {
            return await _personAntiSocialBehaviourCaseNoteApiClient.GetPersonAntiSocialBehaviourCaseNote(id);
        }

        public async Task<PersonAntiSocialBehaviourCaseNoteDto> PostPersonAntiSocialBehaviourCaseNote(PersonAntiSocialBehaviourCaseNoteDto model)
        {
            return await _personAntiSocialBehaviourCaseNoteApiClient.PostPersonAntiSocialBehaviourCaseNote(model);
        }

        public async Task<PersonAntiSocialBehaviourCaseNoteDto> PutPersonAntiSocialBehaviourCaseNote(int id, PersonAntiSocialBehaviourCaseNoteDto model)
        {
            return await _personAntiSocialBehaviourCaseNoteApiClient.PutPersonAntiSocialBehaviourCaseNote(id, model);
        }

        public async Task<HttpResponseMessage> DeletePersonAntiSocialBehaviourCaseNote(int id)
        {
            return await _personAntiSocialBehaviourCaseNoteApiClient.DeletePersonAntiSocialBehaviourCaseNote(id);
        }

        public async Task<PersonCaseSearchModel> GetPersonCases(PersonCaseSearchModel model)
        {
            return await _personCaseApiClient.GetPersonCases(model);
        }

        public async Task<PersonCaseDto> GetPersonCase(int personCaseId)
        {
            return await _personCaseApiClient.GetPersonCase(personCaseId);
        }

        public async Task<PersonCaseDto> PostPersonCase(PersonCaseDto model)
        {
            return await _personCaseApiClient.PostPersonCase(model);
        }

        public async Task<PersonCaseDto> PutPersonCase(int id, PersonCaseDto model)
        {
            return await _personCaseApiClient.PutPersonCase(id,model);
        }

        public async Task<PersonFlagSearchModel> GetPersonFlags(PersonFlagSearchModel model)
        {
            return await _personFlagApiClient.GetPersonFlags( model);
        }

        public async Task<PersonFlagDto> GetPersonFlag(int personFlagId)
        {
            return await _personFlagApiClient.GetPersonFlag(personFlagId);
        }

        public async Task<PersonFlagDto> PostPersonFlag(PersonFlagDto model)
        {
            return await _personFlagApiClient.PostPersonFlag(model);
        }

        public async Task<PersonFlagDto> PutPersonFlag(int id, PersonFlagDto model)
        {
            return await _personFlagApiClient.PutPersonFlag(id,model);
        }

        public async Task<PersonAlertDto> PostPersonAlert(PersonAlertDto model)
        {
            return await _personAlertApiClient.PostPersonAlert(model);
        }

        public async Task<PersonAlertDto> PutPersonAlert(int id, PersonAlertDto model)
        {
            return await _personAlertApiClient.PutPersonAlert(id, model);
        }

        public async Task<List<PersonDto>> GetPersonByVblApplicationId(int vblApplicationId)
        {
            return await _personApiClient.GetPersonByVblApplicationId(vblApplicationId);
        }

        public async Task<List<PersonDto>> GetPersonByMainContactId(int mainContactPersonId)
        {
            return await _personApiClient.GetPersonByMainContactId(mainContactPersonId);
        }

        public async Task<PersonDto> PostPerson(PersonDto model)
        {
            return await _personApiClient.PostPerson(model);
        }

        public async Task<PersonDto> PutPerson(int id, PersonDto model)
        {
            return await _personApiClient.PutPerson(id,model);
        }

        public async Task<PersonDto> UpdatePersonContactDetails(int id, PersonDto model)
        {
            return await _personApiClient.UpdatePersonContactDetails(id, model);
        }

        public async Task<PersonApplicationLinkSearchModel> GetPersonApplicationLinks(PersonApplicationLinkSearchModel model)
        {
            return await _personApplicationLinkApiClient.GetPersonApplicationLinks(model);
        }

        public async Task<PersonApplicationLinkDto> GetPersonApplicationLink(int id)
        {
            return await _personApplicationLinkApiClient.GetPersonApplicationLink(id);
        }

        public async Task<PersonApplicationLinkDto> PostPersonApplicationLink(PersonApplicationLinkDto model)
        {
            return await _personApplicationLinkApiClient.PostPersonApplicationLink( model);
        }

        public async Task<PersonApplicationLinkDto> PutPersonApplicationLink(int id, PersonApplicationLinkDto model)
        {
            return await _personApplicationLinkApiClient.PutPersonApplicationLink(id, model);
        }

        public async Task<AuditSearchModel> GetAudits(AuditSearchModel model)
        {
            return await _auditApiClient.GetAudits(model);
        }

        public async Task<PersonDocumentSearchModel> GetPersonDocuments(PersonDocumentSearchModel model)
        {
            return await _personDocumentApiClient.GetPersonDocuments(model);
        }

        public async Task<PersonDocumentDto> GetPersonDocument(int id)
        {
            return await _personDocumentApiClient.GetPersonDocument(id);
        }

        public async Task<PersonDocumentDto> PostPersonDocument(PersonDocumentDto model)
        {
            return await _personDocumentApiClient.PostPersonDocument(model);
        }

        public async Task<PersonDocumentDto> PutPersonDocument(int id, PersonDocumentDto model)
        {
            return await _personDocumentApiClient.PutPersonDocument(id,model);
        }

        public async Task<PersonEmailSearchModel> GetPersonEmails(PersonEmailSearchModel model)
        {
            return await _personEmailApiClient.GetPersonEmails(model);
        }

        public async Task<PersonEmailDto> GetPersonEmail(int id)
        {
            return await _personEmailApiClient.GetPersonEmail(id);
        }

        public async Task<PersonEmailDto> PostPersonEmail(PersonEmailDto model)
        {
            return await _personEmailApiClient.PostPersonEmail(model);
        }

        public async Task<PersonEmailDto> PutPersonEmail(int id, PersonEmailDto model)
        {
            return await _personEmailApiClient.PutPersonEmail(id,model);
        }

        public async Task<PersonSmsSearchModel> GetPersonSmses(PersonSmsSearchModel model)
        {
            return await _personSmsApiClient.GetPersonSmses(model);
        }

        public async Task<PersonSmsDto> GetPersonSms(int id)
        {
            return await _personSmsApiClient.GetPersonSms(id);
        }

        public async Task<PersonSmsDto> PostPersonSms(PersonSmsDto model)
        {
            return await _personSmsApiClient.PostPersonSms(model);
        }

        public async Task<PersonSmsDto> PutPersonSms(int id, PersonSmsDto model)
        {
            return await _personSmsApiClient.PutPersonSms(id,model);
        }

        public async Task<TenantSearchModel> GetTenants(TenantSearchModel model)
        {
            return await _tenantApiClient.GetTenants(model);
        }

        public async Task<TenantDto> GetTenantByTenantCode(string tenantCode)
        {
            return await _tenantApiClient.GetTenantByTenantCode(tenantCode);
        }

        public async Task<TenantDto> GetTenant(int id)
        {
            return await _tenantApiClient.GetTenant(id);
        }

        public async Task<TenantDto> PostTenant(TenantDto model)
        {
            return await _tenantApiClient.PostTenant(model);
        }

        public async Task<TenantDto> PutTenant(int id, TenantDto model)
        {
            return await _tenantApiClient.PutTenant(id,model);
        }

        public async Task<TenantHistoryViewSearchModel> GetTenantHistoryViews(TenantHistoryViewSearchModel model)
        {
            return await _tenantHistoryViewApiClient.GetTenantHistoryViews(model);
        }

        public async Task<VblApplicationDto> GetVblApplication(int applicationId)
        {
            return await _vblApplicationApiClient.GetVblApplication(applicationId);
        }


        public async Task<PersonAlertCommentSearchModel> GetPersonAlertComments(PersonAlertCommentSearchModel model)
        {
            return await _personAlertCommentApiClient.GetPersonAlertComments(model);
        }

        public async Task<PersonAlertCommentDto> GetPersonAlertComment(int personAlertCommentId)
        {
            return await _personAlertCommentApiClient.GetPersonAlertComment(personAlertCommentId);
        }

        public async Task<PersonAlertCommentDto> PostPersonAlertComment(PersonAlertCommentDto model)
        {
            return await _personAlertCommentApiClient.PostPersonAlertComment(model);
        }

        public async Task<PersonAlertCommentDto> PutPersonAlertComment(int id, PersonAlertCommentDto model)
        {
            return await _personAlertCommentApiClient.PutPersonAlertComment(id,model);
        }

        public async Task<PersonFlagCommentSearchModel> GetPersonFlagComments(PersonFlagCommentSearchModel model)
        {
            return await _personFlagCommentApiClient.GetPersonFlagComments( model);
        }

        public async Task<PersonFlagCommentDto> GetPersonFlagComment(int personFlagCommentId)
        {
            return await _personFlagCommentApiClient.GetPersonFlagComment(personFlagCommentId);
        }

        public async Task<PersonFlagCommentDto> PostPersonFlagComment(PersonFlagCommentDto model)
        {
            return await _personFlagCommentApiClient.PostPersonFlagComment(model);
        }

        public async Task<PersonFlagCommentDto> PutPersonFlagComment(int id, PersonFlagCommentDto model)
        {
            return await _personFlagCommentApiClient.PutPersonFlagComment(id,model);
        }
    }
}
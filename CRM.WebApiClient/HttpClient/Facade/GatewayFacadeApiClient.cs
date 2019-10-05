using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Customer;
using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Employee;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;

namespace CRM.WebApiClient.HttpClient.Facade
{
    public class GatewayFacadeApiClient: IGatewayFacadeApiClient
    {
        private readonly IAuditApiClient _auditApiClient;
        private readonly ILookupApiClient _lookupApiClient;
        private readonly IPersonApiClient _personApiClient;

        public GatewayFacadeApiClient(IAuditApiClient auditApiClient,ILookupApiClient lookupApiClient,IPersonApiClient personApiClient)
        {
            _auditApiClient = auditApiClient;
            _lookupApiClient = lookupApiClient;
            _personApiClient = personApiClient;
        }
        public Task<AuditSearchModel> GetAudits(AuditSearchModel model)
        {
           return _auditApiClient.GetAudits(model);
        }

        public Task<LookupDto> GetLookup()
        {
            return _lookupApiClient.GetLookup();
        }

        public async Task<LookupSearch> GetLookupUsingOdata(List<string> entities)
        {
            return await _lookupApiClient.GetLookupUsingOdata(entities);
        }

        public async Task<PersonSearchModel> GetPersons(PersonSearchModel model)
        {
            return await _personApiClient.GetPersons(model);
        }

        public async Task<PersonDto> GetPerson(string globalIdentityKey)
        {
            return await _personApiClient.GetPerson(globalIdentityKey);
        }

        public async Task<PersonDto> GetPerson(int personId)
        {
            return await _personApiClient.GetPerson(personId);
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

        public async Task<PersonDto> PutPerson(int id,PersonDto model)
        {
            return await _personApiClient.PutPerson(id, model);
        }

        public async Task<PersonDto> UpdatePersonContactDetails(int id, PersonDto model)
        {
            return await _personApiClient.UpdatePersonContactDetails(id, model);
        }
    }
}
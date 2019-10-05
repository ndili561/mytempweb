using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonApiClient
    {
        Task<PersonSearchModel> GetPersons(PersonSearchModel model);
        Task<PersonDto> GetPerson(string globalIdentityKey);
        Task<PersonDto> GetPerson(int personId);
        Task<List<PersonDto>> GetPersonByVblApplicationId(int vblApplicationId);
        Task<List<PersonDto>> GetPersonByMainContactId(int mainContactPersonId);
        Task<PersonDto> PostPerson(PersonDto model);
        Task<PersonDto> PutPerson(int id, PersonDto model);
        Task<PersonDto> UpdatePersonContactDetails(int id, PersonDto model);

    }
}
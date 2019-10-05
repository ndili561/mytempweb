using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonFlagApiClient
    {
        Task<PersonFlagSearchModel> GetPersonFlags(PersonFlagSearchModel model);
        Task<PersonFlagDto> GetPersonFlag(int personFlagId);
        Task<PersonFlagDto> PostPersonFlag(PersonFlagDto model);
        Task<PersonFlagDto> PutPersonFlag(int id, PersonFlagDto model);
    }
}
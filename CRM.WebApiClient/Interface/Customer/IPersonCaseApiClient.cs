using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonCaseApiClient
    {
        Task<PersonCaseSearchModel> GetPersonCases(PersonCaseSearchModel model);
        Task<PersonCaseDto> GetPersonCase(int personCaseId);
        Task<PersonCaseDto> PostPersonCase(PersonCaseDto model);
        Task<PersonCaseDto> PutPersonCase(int id, PersonCaseDto model);
    }
}
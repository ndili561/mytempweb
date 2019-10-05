using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IPersonCaseTypeApiClient
    {
        Task<PersonCaseTypeSearchModel> GetCaseTypes(PersonCaseTypeSearchModel model);
        Task<PersonCaseTypeDto> GetCaseType(int id);
        Task<PersonCaseTypeDto> PostCaseType(PersonCaseTypeDto model);
        Task<PersonCaseTypeDto> PutCaseType(int id, PersonCaseTypeDto model);
    }
}
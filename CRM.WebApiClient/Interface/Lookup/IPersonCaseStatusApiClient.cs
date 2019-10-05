using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IPersonCaseStatusApiClient
    {
        Task<PersonCaseStatusSearchModel> GetCaseStatuses(PersonCaseStatusSearchModel model);
        Task<PersonCaseStatusDto> GetCaseStatus(int id);
        Task<PersonCaseStatusDto> PostCaseStatus(PersonCaseStatusDto model);
        Task<PersonCaseStatusDto> PutCaseStatus(int id, PersonCaseStatusDto model);
    }
}
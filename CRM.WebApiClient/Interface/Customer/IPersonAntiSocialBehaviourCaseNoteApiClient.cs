using System.Net.Http;
using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonAntiSocialBehaviourCaseNoteApiClient
    {
        Task<PersonAntiSocialBehaviourCaseNoteSearchModel> GetPersonAntiSocialBehaviourCaseNotes(PersonAntiSocialBehaviourCaseNoteSearchModel model);
        Task<PersonAntiSocialBehaviourCaseNoteDto> GetPersonAntiSocialBehaviourCaseNote(int id);
        Task<PersonAntiSocialBehaviourCaseNoteDto> PostPersonAntiSocialBehaviourCaseNote(PersonAntiSocialBehaviourCaseNoteDto model);
        Task<PersonAntiSocialBehaviourCaseNoteDto> PutPersonAntiSocialBehaviourCaseNote(int id, PersonAntiSocialBehaviourCaseNoteDto model);
        Task<HttpResponseMessage> DeletePersonAntiSocialBehaviourCaseNote(int id);
    }
}
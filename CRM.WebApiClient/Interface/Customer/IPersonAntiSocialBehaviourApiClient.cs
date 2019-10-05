using System.Net.Http;
using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonAntiSocialBehaviourApiClient
    {
        Task<PersonAntiSocialBehaviourSearchModel> GetPersonAntiSocialBehaviours(PersonAntiSocialBehaviourSearchModel model);
        Task<PersonAntiSocialBehaviourDto> GetPersonAntiSocialBehaviour(int id);
        Task<PersonAntiSocialBehaviourDto> PostPersonAntiSocialBehaviour(PersonAntiSocialBehaviourDto model);
        Task<PersonAntiSocialBehaviourDto> PutPersonAntiSocialBehaviour(int id, PersonAntiSocialBehaviourDto model);
        Task<HttpResponseMessage> DeletePersonAntiSocialBehaviour(int id);
    }
}
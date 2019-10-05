using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IAntiSocialBehaviourTypeApiClient
    {
        Task<AntiSocialBehaviourTypeSearchModel> GetAntiSocialBehaviourTypes(AntiSocialBehaviourTypeSearchModel model);
        Task<AntiSocialBehaviourTypeDto> GetAntiSocialBehaviourType(int id);
        Task<AntiSocialBehaviourTypeDto> PostAntiSocialBehaviourType(AntiSocialBehaviourTypeDto model);
        Task<AntiSocialBehaviourTypeDto> PutAntiSocialBehaviourType(int id, AntiSocialBehaviourTypeDto model);
    }
}
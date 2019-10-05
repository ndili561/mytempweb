using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IAntiSocialBehaviourCaseStatusApiClient
    {
        Task<AntiSocialBehaviourCaseStatusSearchModel> GetAntiSocialBehaviourCaseStatuses(AntiSocialBehaviourCaseStatusSearchModel model);
        Task<AntiSocialBehaviourCaseStatusDto> GetAntiSocialBehaviourCaseStatus(int id);
        Task<AntiSocialBehaviourCaseStatusDto> PostAntiSocialBehaviourCaseStatus(AntiSocialBehaviourCaseStatusDto model);
        Task<AntiSocialBehaviourCaseStatusDto> PutAntiSocialBehaviourCaseStatus(int id, AntiSocialBehaviourCaseStatusDto model);
    }
}
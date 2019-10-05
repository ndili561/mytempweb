using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IAntiSocialBehaviourCaseClosureReasonApiClient
    {
        Task<AntiSocialBehaviourCaseClosureReasonSearchModel> GetAntiSocialBehaviourCaseClosureReasons(AntiSocialBehaviourCaseClosureReasonSearchModel model);
        Task<AntiSocialBehaviourCaseClosureReasonDto> GetAntiSocialBehaviourCaseClosureReason(int id);
        Task<AntiSocialBehaviourCaseClosureReasonDto> PostAntiSocialBehaviourCaseClosureReason(AntiSocialBehaviourCaseClosureReasonDto model);
        Task<AntiSocialBehaviourCaseClosureReasonDto> PutAntiSocialBehaviourCaseClosureReason(int id, AntiSocialBehaviourCaseClosureReasonDto model);
    }
}
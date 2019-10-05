using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IPriorityApiClient
    {
        Task<PrioritySearchModel> GetPriorities(PrioritySearchModel model);
        Task<PriorityDto> GetPriority(int id);
        Task<PriorityDto> PostPriority(PriorityDto model);
        Task<PriorityDto> PutPriority(int id, PriorityDto model);
    }
}
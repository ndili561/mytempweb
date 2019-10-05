using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IFlagGroupApiClient
    {
        Task<FlagGroupSearchModel> GetFlagGroups(FlagGroupSearchModel model);
        Task<FlagGroupDto> GetFlagGroup(int id);
        Task<FlagGroupDto> PostFlagGroup(FlagGroupDto model);
        Task<FlagGroupDto> PutFlagGroup(int id, FlagGroupDto model);
    }
}
using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IFlagTypeApiClient
    {
        Task<FlagTypeSearchModel> GetFlagTypes(FlagTypeSearchModel model);
        Task<FlagTypeDto> GetFlagType(int id);
        Task<FlagTypeDto> PostFlagType(FlagTypeDto model);
        Task<FlagTypeDto> PutFlagType(int id, FlagTypeDto model);
    }
}
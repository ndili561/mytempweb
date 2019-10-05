using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface ILookupApiClient
    {
        Task<LookupDto> GetLookup();
        Task<LookupSearch> GetLookupUsingOdata(List<string> entities);
    }
}
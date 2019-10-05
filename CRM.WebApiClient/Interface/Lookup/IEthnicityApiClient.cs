using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IEthnicityApiClient
    {
        Task<EthnicitySearchModel> GetEthnicities(EthnicitySearchModel model);
        Task<EthnicityDto> GetEthnicity(int ethnicityId);
        Task<EthnicityDto> PostEthnicity(EthnicityDto model);
        Task<EthnicityDto> PutEthnicity(int id, EthnicityDto model);
    }
}
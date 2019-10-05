using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IGenderApiClient
    {
        Task<GenderSearchModel> GetGenders(GenderSearchModel model);
        Task<GenderDto> GetGender(int genderId);
        Task<GenderDto> PostGender(GenderDto model);
        Task<GenderDto> PutGender(int id, GenderDto model);
    }
}
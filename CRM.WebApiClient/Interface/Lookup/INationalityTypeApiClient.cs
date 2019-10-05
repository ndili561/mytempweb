using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface INationalityTypeApiClient
    {
        Task<NationalityTypeSearchModel> GetNationalityTypes(NationalityTypeSearchModel model);
        Task<NationalityTypeDto> GetNationalityType(int nationalityTypeId);
        Task<NationalityTypeDto> PostNationalityType(NationalityTypeDto model);
        Task<NationalityTypeDto> PutNationalityType(int id, NationalityTypeDto model);
    }
}
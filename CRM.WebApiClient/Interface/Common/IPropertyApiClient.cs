using System.Threading.Tasks;
using CRM.Entity.Model.Asset;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;

namespace CRM.WebApiClient.Interface.Common
{
    /// <summary>
    /// </summary>
    public interface IPropertyApiClient
    {
        Task<AssetModel> GetAsset(int assetId);
        Task<PropertySearchModel> GetProperties(PropertySearchModel model);
        Task<PropertyDto> GetProperty(int id);
        Task<PropertyModel> GetPropertyDetails(string propertyCode);
       
        Task<VanLocationSearchModel> GetVanLocations(VanLocationSearchModel model);

    }
}
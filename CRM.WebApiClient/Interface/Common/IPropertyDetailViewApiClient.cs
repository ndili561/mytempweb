using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;

namespace CRM.WebApiClient.Interface.Common
{
    /// <summary>
    /// </summary>
    public interface IPropertyDetailViewApiClient
    {
        Task<PropertyDetailViewSearchModel> GetPropertyDetailViews(PropertyDetailViewSearchModel model);
        Task<PropertyDetailViewDto> GetPropertyDetailView(string propertyCode);
    }
}
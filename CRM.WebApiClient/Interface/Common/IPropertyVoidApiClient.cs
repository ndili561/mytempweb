using System.Threading.Tasks;
using CRM.Entity.Search.Common;

namespace CRM.WebApiClient.Interface.Common
{
    /// <summary>
    /// </summary>
    public interface IPropertyVoidApiClient
    {
        Task<PropertyVoidSearchModel> GetPropertyVoids(PropertyVoidSearchModel model);
        Task<PropertyVoidViewSearchModel> GetPropertyVoidViews(PropertyVoidViewSearchModel model);

    }
}
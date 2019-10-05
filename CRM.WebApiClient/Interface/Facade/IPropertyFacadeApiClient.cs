using CRM.WebApiClient.Interface.Common;
using CRM.WebApiClient.Interface.Customer;

namespace CRM.WebApiClient.Interface.Facade
{
    /// <summary>
    /// </summary>
    public interface IPropertyFacadeApiClient:
        IPropertyApiClient,
        IPropertyVoidApiClient,
        IPropertyDocumentApiClient,
        IPropertyDetailViewApiClient,
        IRepairApiClient,
        ITenantApiClient,
        ITenantHistoryViewApiClient
    {
    }
}
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Employee;
using CRM.WebApiClient.Interface.Lookup;

namespace CRM.WebApiClient.Interface.Facade
{
    /// <summary>
    /// </summary>
    public interface IGatewayFacadeApiClient: IAuditApiClient,ILookupApiClient,IPersonApiClient
    {
    }
}
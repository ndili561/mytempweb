using CRM.WebApiClient.Interface.Common;

namespace CRM.WebApiClient.Interface.Facade
{
    /// <summary>
    /// </summary>
    public interface ICommonFacadeApiClient: 
        IDocumentApiClient, 
        IEmailApiClient,
        ISmsApiClient
    {
    }
}
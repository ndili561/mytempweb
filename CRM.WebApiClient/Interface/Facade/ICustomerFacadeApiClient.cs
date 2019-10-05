using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Employee;

namespace CRM.WebApiClient.Interface.Facade
{
    /// <summary>
    /// </summary>
    public interface ICustomerFacadeApiClient :
    IAddressApiClient,
    IAuditApiClient,
    IPersonAddressApiClient,
    IPersonAlertApiClient,
    IPersonAlertCommentApiClient,
    IPersonFlagApiClient,
    IPersonFlagCommentApiClient,
    IPersonCaseApiClient,
    IPersonApiClient,
    IPersonAntiSocialBehaviourApiClient,
    IPersonAntiSocialBehaviourCaseNoteApiClient,
    IPersonApplicationLinkApiClient,
    IPersonDocumentApiClient,
    IPersonEmailApiClient,
    IPersonSmsApiClient,
    ITenantApiClient,
    ITenantHistoryViewApiClient,
    IVblApplicationApiClient
    {
    }
}
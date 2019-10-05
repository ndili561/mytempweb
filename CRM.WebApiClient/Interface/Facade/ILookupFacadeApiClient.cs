using CRM.WebApiClient.Interface.Lookup;

namespace CRM.WebApiClient.Interface.Facade
{
    /// <summary>
    /// </summary>
    public interface ILookupFacadeApiClient: 
        IAddressTypeApiClient,
        IAlertTypeApiClient,
        IAlertGroupApiClient,
        IAlertStatusApiClient,
        IAntiSocialBehaviourCaseClosureReasonApiClient,
        IAntiSocialBehaviourCaseStatusApiClient,
        IAntiSocialBehaviourTypeApiClient,
        IPersonCaseTypeApiClient,
        IPersonCaseStatusApiClient,
        IDocumentTypeApiClient,
        IEthnicityApiClient,
        IFlagTypeApiClient,
        IFlagGroupApiClient,
        IGenderApiClient,
        ILanguageApiClient,
        IPriorityApiClient,
        IRelationshipApiClient,
        INationalityTypeApiClient,
        ITaskNotificationTypeApiClient,
        ITaskReminderTypeApiClient,
        ITaskStatusApiClient,
        ITitleApiClient
    {
    }
}
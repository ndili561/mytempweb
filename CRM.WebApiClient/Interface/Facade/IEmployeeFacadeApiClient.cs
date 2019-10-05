using CRM.WebApiClient.Interface.Employee;

namespace CRM.WebApiClient.Interface.Facade
{
    /// <summary>
    /// </summary>
    public interface IEmployeeFacadeApiClient:
        IApplicationRoleApiClient,
        IPermissionApiClient,
        ITaskApiClient,
        IChatMessageApiClient,
        IMenuItemApiClient,
        IMessageApiClient,
        IRoleApiClient,
        IUserActivityApiClient,
        IUserApplicationRoleApiClient,
        IUserPersonTaskApiClient,
        IUserTaskNotificationApiClient,
        IUserTaskReminderApiClient,
        IUserApiClient,
        IUserDiaryApiClient,
        IUserGroupApiClient,
        IUserMessageApiClient,
        IUserDocumentApiClient,
        IUserEmailApiClient,
        IUserSmsApiClient,
        IWebApplicationApiClient
    {
    }
}
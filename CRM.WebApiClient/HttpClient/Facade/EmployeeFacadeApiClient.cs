using System.Net.Http;
using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;
using CRM.WebApiClient.Interface.Employee;
using CRM.WebApiClient.Interface.Facade;

namespace CRM.WebApiClient.HttpClient.Facade
{
    public class EmployeeFacadeApiClient: IEmployeeFacadeApiClient
    {
        private readonly IApplicationRoleApiClient _applicationRoleApiClient;
        private readonly IPermissionApiClient _PermissionApiClient;
        private readonly ITaskApiClient _calendarTaskApiClient;
        private readonly IChatMessageApiClient _chatMessageApiClient;
        private readonly IMenuItemApiClient _menuItemApiClient;
        private readonly IMessageApiClient _messageApiClient;
        private readonly IRoleApiClient _roleApiClient;
        private readonly IUserActivityApiClient _userActivityApiClient;
        private readonly IUserApplicationRoleApiClient _userApplicationRoleApiClient;
        private readonly IUserPersonTaskApiClient _userTaskApiClient;
        private readonly IUserTaskNotificationApiClient _userCalendarTaskNotificationApiClient;
        private readonly IUserTaskReminderApiClient _userCalendarTaskReminderApiClient;
        private readonly IUserApiClient _userApiClient;
        private readonly IUserDiaryApiClient _userDiaryApiClient;
        private readonly IUserGroupApiClient _userGroupApiClient;
        private readonly IUserMessageApiClient _userMessageApiClient;
        private readonly IUserDocumentApiClient _userDocumentApiClient;
        private readonly IUserEmailApiClient _userEmailApiClient;
        private readonly IUserSmsApiClient _userSmsApiClient;




        private readonly IWebApplicationApiClient _webApplicationApiClient;

        public EmployeeFacadeApiClient(
        IApplicationRoleApiClient applicationRoleApiClient,
        IPermissionApiClient PermissionApiClient,  
        ITaskApiClient calendarTaskApiClient,  
        IChatMessageApiClient chatMessageApiClient,  
        IMenuItemApiClient menuItemApiClient,  
        IMessageApiClient messageApiClient,  
        IRoleApiClient roleApiClient,  
        IUserActivityApiClient userActivityApiClient,  
        IUserApplicationRoleApiClient userApplicationRoleApiClient,  
        IUserPersonTaskApiClient userTaskApiClient,  
        IUserTaskNotificationApiClient userCalendarTaskNotificationApiClient,  
        IUserTaskReminderApiClient userCalendarTaskReminderApiClient,  
        IUserApiClient userApiClient, 
        IUserDiaryApiClient userDiaryApiClient, 
        IUserGroupApiClient userGroupApiClient, 
        IUserMessageApiClient userMessageApiClient,
        IUserDocumentApiClient userDocumentApiClient,
        IUserEmailApiClient userEmailApiClient,
        IUserSmsApiClient userSmsApiClient,
        IWebApplicationApiClient webApplicationApiClient 
         )
        {
            _applicationRoleApiClient = applicationRoleApiClient;
            _PermissionApiClient = PermissionApiClient;
            _calendarTaskApiClient = calendarTaskApiClient;
            _chatMessageApiClient = chatMessageApiClient;
            _menuItemApiClient = menuItemApiClient;
            _messageApiClient = messageApiClient;
            _roleApiClient = roleApiClient;
            _userActivityApiClient = userActivityApiClient;
            _userApplicationRoleApiClient = userApplicationRoleApiClient;
            _userTaskApiClient = userTaskApiClient;
            _userCalendarTaskNotificationApiClient = userCalendarTaskNotificationApiClient;
            _userCalendarTaskReminderApiClient = userCalendarTaskReminderApiClient;
            _userApiClient = userApiClient;
            _userDiaryApiClient = userDiaryApiClient;
            _userGroupApiClient = userGroupApiClient;
            _userMessageApiClient = userMessageApiClient;
            _userDocumentApiClient = userDocumentApiClient;
            _userSmsApiClient = userSmsApiClient;
            _userEmailApiClient = userEmailApiClient;
            _webApplicationApiClient = webApplicationApiClient;

        }
     

        public async Task<ApplicationRoleSearchModel> GetApplicationRoles(ApplicationRoleSearchModel model)
        {
            return await _applicationRoleApiClient.GetApplicationRoles(model);
        }

        public async Task<ApplicationRoleDto> GetApplicationRole(int id)
        {
            return await _applicationRoleApiClient.GetApplicationRole(id);
        }

        public async Task<ApplicationRoleDto> PostApplicationRole(ApplicationRoleDto model)
        {
            return await _applicationRoleApiClient.PostApplicationRole(model);
        }

        public async Task<ApplicationRoleDto> PutApplicationRole(int id, ApplicationRoleDto model)
        {
            return await _applicationRoleApiClient.PutApplicationRole(id,model);
        }

        public async Task<PermissionSearchModel> GetPermissions(PermissionSearchModel model)
        {
            return await _PermissionApiClient.GetPermissions( model);
        }

        public async Task<PermissionDto> GetPermission(int id)
        {
            return await _PermissionApiClient.GetPermission(id);
        }

        public async Task<PermissionDto> PostPermission(PermissionDto model)
        {
            return await _PermissionApiClient.PostPermission(model);
        }

        public async Task<PermissionDto> PutPermission(int id, PermissionDto model)
        {
            return await _PermissionApiClient.PutPermission(id, model);
        }

        public async Task<TaskSearchModel> GetTasks(TaskSearchModel model)
        {
            return await _calendarTaskApiClient.GetTasks(model);
        }

        public async Task<TaskDto> GetTask(int id)
        {
            return await _calendarTaskApiClient.GetTask(id);
        }

        public async Task<TaskDto> PostTask(TaskDto model)
        {
            return await _calendarTaskApiClient.PostTask(model);
        }

        public async Task<TaskDto> PutTask(int id, TaskDto model)
        {
            return await _calendarTaskApiClient.PutTask(id, model);
        }

        public async Task<ChatMessageSearchModel> GetChatMessages(ChatMessageSearchModel model)
        {
            return await _chatMessageApiClient.GetChatMessages(model);
        }

        public async Task<ChatMessageDto> GetChatMessage(int id)
        {
            return await _chatMessageApiClient.GetChatMessage(id);
        }

        public async Task<ChatMessageDto> PostChatMessage(ChatMessageDto model)
        {
            return await _chatMessageApiClient.PostChatMessage(model);
        }

        public async Task<ChatMessageDto> PutChatMessage(int id, ChatMessageDto model)
        {
            return await _chatMessageApiClient.PutChatMessage(id,model);
        }

        public async Task<MenuItemSearchModel> GetMenuItems(MenuItemSearchModel model)
        {
            return await _menuItemApiClient.GetMenuItems( model);
        }

        public async Task<MenuItemDto> GetMenuItem(int id)
        {
            return await _menuItemApiClient.GetMenuItem(id);
        }

        public async Task<MenuItemDto> PostMenuItem(MenuItemDto model)
        {
            return await _menuItemApiClient.PostMenuItem(model);
        }

        public async Task<MenuItemDto> PutMenuItem(int id, MenuItemDto model)
        {
            return await _menuItemApiClient.PutMenuItem(id,model);
        }

        public async Task<MessageSearchModel> GetMessages(MessageSearchModel model)
        {
            return await _messageApiClient.GetMessages( model);
        }

        public async Task<MessageDto> GetMessage(int id)
        {
            return await _messageApiClient.GetMessage(id);
        }

        public async Task<MessageDto> PostMessage(MessageDto model)
        {
            return await _messageApiClient.PostMessage(model);
        }

        public async Task<MessageDto> PutMessage(int id, MessageDto model)
        {
            return await _messageApiClient.PutMessage(id,model);
        }

        public async Task<RoleSearchModel> GetRoles(RoleSearchModel model)
        {
            return await _roleApiClient.GetRoles( model);
        }

        public async Task<RoleDto> GetRole(int id)
        {
            return await _roleApiClient.GetRole(id);
        }

        public async Task<RoleDto> PostRole(RoleDto model)
        {
            return await _roleApiClient.PostRole(model);
        }

        public async Task<RoleDto> PutRole(int id, RoleDto model)
        {
            return await _roleApiClient.PutRole(id,model);
        }

        public async Task<UserActivitySearchModel> GetUserActivities(UserActivitySearchModel model)
        {
            return await _userActivityApiClient.GetUserActivities( model);
        }

        public async Task<UserActivityDto> GetUserActivity(int id)
        {
            return await _userActivityApiClient.GetUserActivity(id);
        }

        public async Task<UserActivityDto> PostUserActivity(UserActivityDto model)
        {
            return await _userActivityApiClient.PostUserActivity(model);
        }

        public async Task<UserActivityDto> PutUserActivity(int id, UserActivityDto model)
        {
            return await _userActivityApiClient.PutUserActivity(id,model);
        }

        public async Task<UserApplicationRoleSearchModel> GetUserApplicationRoles(UserApplicationRoleSearchModel model)
        {
            return await _userApplicationRoleApiClient.GetUserApplicationRoles(model);
        }

        public async Task<UserApplicationRoleDto> GetUserApplicationRole(int id)
        {
            return await _userApplicationRoleApiClient.GetUserApplicationRole(id);
        }

        public async Task<UserApplicationRoleDto> PostUserApplicationRole(UserApplicationRoleDto model)
        {
            return await _userApplicationRoleApiClient.PostUserApplicationRole(model);
        }

        public async Task<UserApplicationRoleDto> PutUserApplicationRole(int id, UserApplicationRoleDto model)
        {
            return await _userApplicationRoleApiClient.PutUserApplicationRole(id,model);
        }

        public async Task<UserTaskSearchModel> GetUserTasks(UserTaskSearchModel model)
        {
            return await _userTaskApiClient.GetUserTasks( model);
        }

        public async Task<UserPersonTaskDto> GetUserTask(int id)
        {
            return await _userTaskApiClient.GetUserTask(id);
        }

        public async Task<UserDto> GetUserTaskCalendarFile(int employeeId)
        {
            return await _userTaskApiClient.GetUserTaskCalendarFile(employeeId);
        }

        public async Task<UserDto> PutUserTaskCalendarFileUpload(int employeeId, UserDto model)
        {
            return await _userTaskApiClient.PutUserTaskCalendarFileUpload(employeeId, model);
        }

        public async Task<UserPersonTaskDto> PostUserTask(UserPersonTaskDto model)
        {
            return await _userTaskApiClient.PostUserTask(model);
        }

        public async Task<UserPersonTaskDto> PutUserTask(int id, UserPersonTaskDto model)
        {
            return await _userTaskApiClient.PutUserTask(id,model);
        }

        public async Task<HttpResponseMessage> DeleteUserTask(int id)
        {
            return await _userTaskApiClient.DeleteUserTask(id);
        }

        public async Task<UserTaskNotificationSearchModel> GetUserTaskNotifications(UserTaskNotificationSearchModel model)
        {
            return await _userCalendarTaskNotificationApiClient.GetUserTaskNotifications(model);
        }

        public async Task<UserTaskNotificationDto> GetUserTaskNotification(int id)
        {
            return await _userCalendarTaskNotificationApiClient.GetUserTaskNotification(id);
        }

        public async Task<UserTaskNotificationDto> PostUserTaskNotification(UserTaskNotificationDto model)
        {
            return await _userCalendarTaskNotificationApiClient.PostUserTaskNotification(model);
        }

        public async Task<UserTaskNotificationDto> PutUserTaskNotification(int id, UserTaskNotificationDto model)
        {
            return await _userCalendarTaskNotificationApiClient.PutUserTaskNotification(id, model);
        }

        public async Task<UserTaskReminderSearchModel> GetUserTaskReminders(UserTaskReminderSearchModel model)
        {
            return await _userCalendarTaskReminderApiClient.GetUserTaskReminders( model);
        }

        public async Task<UserTaskReminderDto> GetUserTaskReminder(int id)
        {
            return await _userCalendarTaskReminderApiClient.GetUserTaskReminder(id);
        }

        public async Task<UserTaskReminderDto> PostUserTaskReminder(UserTaskReminderDto model)
        {
            return await _userCalendarTaskReminderApiClient.PostUserTaskReminder(model);
        }

        public async Task<UserTaskReminderDto> PutUserTaskReminder(int id, UserTaskReminderDto model)
        {
            return await _userCalendarTaskReminderApiClient.PutUserTaskReminder(id,model);
        }

        public async Task<UserSearchModel> GetUsersWithTasks(UserSearchModel model)
        {
            return await _userApiClient.GetUsersWithTasks(model);
        }

        public async Task<UserSearchModel> GetUsers(UserSearchModel model)
        {
            return await _userApiClient.GetUsers( model);
        }

        public async Task<UserDto> GetUser(int id)
        {
            return await _userApiClient.GetUser(id);
        }

        public async Task<UserDto> PostUser(UserDto model)
        {
            return await _userApiClient.PostUser(model);
        }

        public async Task<UserDto> PutUser(int id, UserDto model)
        {
            return await _userApiClient.PutUser(id,model);
        }

        public async Task<UserDto> PatchUserTasks(int id, UserDto model)
        {
            return await _userApiClient.PatchUserTasks(id, model);
        }

        public async Task<UserDiarySearchModel> GetUserDiarys(UserDiarySearchModel model)
        {
            return await _userDiaryApiClient.GetUserDiarys(model);
        }

        public async Task<UserDiaryDto> GetUserDiary(int id)
        {
            return await _userDiaryApiClient.GetUserDiary(id);
        }

        public async Task<UserDiaryDto> PostUserDiary(UserDiaryDto model)
        {
            return await _userDiaryApiClient.PostUserDiary(model);
        }

        public async Task<UserDiaryDto> PutUserDiary(int id, UserDiaryDto model)
        {
            return await _userDiaryApiClient.PutUserDiary(id,model);
        }

        public async Task<UserGroupSearchModel> GetUserGroups(UserGroupSearchModel model)
        {
            return await _userGroupApiClient.GetUserGroups( model);
        }

        public async Task<UserGroupDto> GetUserGroup(int id)
        {
            return await _userGroupApiClient.GetUserGroup(id);
        }

        public async Task<UserGroupDto> PostUserGroup(UserGroupDto model)
        {
            return await _userGroupApiClient.PostUserGroup(model);
        }

        public async Task<UserGroupDto> PutUserGroup(int id, UserGroupDto model)
        {
            return await _userGroupApiClient.PutUserGroup(id,model);
        }

        public async Task<UserMessageSearchModel> GetUserMessages(UserMessageSearchModel model)
        {
            return await _userMessageApiClient.GetUserMessages( model);
        }

        public async Task<UserMessageDto> GetUserMessage(int id)
        {
            return await _userMessageApiClient.GetUserMessage(id);
        }

        public async Task<UserMessageDto> PostUserMessage(UserMessageDto model)
        {
            return await _userMessageApiClient.PostUserMessage(model);
        }

        public async Task<UserMessageDto> PutUserMessage(int id, UserMessageDto model)
        {
            return await _userMessageApiClient.PutUserMessage(id,model);
        }

        public async Task<WebApplicationSearchModel> GetWebApplications(WebApplicationSearchModel model)
        {
            return await _webApplicationApiClient.GetWebApplications(model);
        }

        public async Task<WebApplicationDto> GetWebApplication(int id)
        {
            return await _webApplicationApiClient.GetWebApplication(id);
        }

        public async Task<WebApplicationDto> PostWebApplication(WebApplicationDto model)
        {
            return await _webApplicationApiClient.PostWebApplication(model);
        }

        public async Task<WebApplicationDto> PutWebApplication(int id, WebApplicationDto model)
        {
            return await _webApplicationApiClient.PutWebApplication(id,model);
        }

        public async Task<UserDocumentSearchModel> GetUserDocuments(UserDocumentSearchModel model)
        {
            return await _userDocumentApiClient.GetUserDocuments(model);
        }

        public async Task<UserDocumentDto> GetUserDocument(int id)
        {
            return await _userDocumentApiClient.GetUserDocument(id);
        }

        public async Task<UserDocumentDto> PostUserDocument(UserDocumentDto model)
        {
            return await _userDocumentApiClient.PostUserDocument(model);
        }

        public async Task<UserDocumentDto> PutUserDocument(int id, UserDocumentDto model)
        {
            return await _userDocumentApiClient.PutUserDocument(id, model);
        }

        public async Task<UserEmailSearchModel> GetUserEmails(UserEmailSearchModel model)
        {
            return await _userEmailApiClient.GetUserEmails(model);
        }

        public async Task<UserEmailDto> GetUserEmail(int id)
        {
            return await _userEmailApiClient.GetUserEmail(id);
        }

        public async Task<UserEmailDto> PostUserEmail(UserEmailDto model)
        {
            return await _userEmailApiClient.PostUserEmail(model);
        }

        public async Task<UserEmailDto> PutUserEmail(int id, UserEmailDto model)
        {
            return await _userEmailApiClient.PutUserEmail(id, model);
        }

        public async Task<UserSmsSearchModel> GetUserSmses(UserSmsSearchModel model)
        {
            return await _userSmsApiClient.GetUserSmses(model);
        }

        public async Task<UserSmsDto> GetUserSms(int id)
        {
            return await _userSmsApiClient.GetUserSms(id);
        }

        public async Task<UserSmsDto> PostUserSms(UserSmsDto model)
        {
            return await _userSmsApiClient.PostUserSms(model);
        }

        public async Task<UserSmsDto> PutUserSms(int id, UserSmsDto model)
        {
            return await _userSmsApiClient.PutUserSms(id, model);
        }
    }
}
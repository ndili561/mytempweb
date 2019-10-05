using AutoMapper;
using CRM.DAL.Database;
using CRM.DAL.Repository;
using CRM.WebAPI.BLL;
using CRM.WebAPI.BLL.Interface;
using CRM.WebAPI.InitializeApplication.Automapper;
using CRM.WebAPI.Services.Common;
using CRM.WebAPI.Services.Customer;
using CRM.WebAPI.Services.Employee;
using CRM.WebAPI.Services.Interfaces.Common;
using CRM.WebAPI.Services.Interfaces.Customer;
using CRM.WebAPI.Services.Interfaces.Employee;
using CRM.WebAPI.Services.Interfaces.Lookup;
using CRM.WebAPI.Services.Lookup;
using CRM.WebAPI.Services.OData;
using DocumentApi.HttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.WebAPI
{
    public static class BootstrapService
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(x => x.AddProfile(new InitializeAutomapperProfile()));//https://dotnetcoretutorials.com/2017/09/23/using-automapper-asp-net-core/
          
            services.AddCors();
            InitializeServices(services);
        }
        private static void InitializeServices(IServiceCollection services)
        {
            #region Common
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IPdfService, PdfService>();
           // services.AddScoped<IMvcRazorToPdf, MvcRazorToPdf>();
            services.AddScoped<IDocumentApiClient, DocumentApiClient>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IPropertyDetailViewService, PropertyDetailViewService>();
            #endregion

            #region Employee 

            services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApplicationTaskService, ApplicationTaskService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IChatMessageService, ChatMessageService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserActivityService, UserActivityService>();
            services.AddScoped<IUserApplicationRoleService, UserApplicationRoleService>();
            services.AddScoped<IUserTaskNotificationService, UserTaskNotificationService>();
            services.AddScoped<IUserTaskReminderService, UserTaskReminderService>();
            services.AddScoped<IUserPersonTaskService, UserPersonTaskService>();
            services.AddScoped<IUserDiaryService, UserDiaryService>();
            services.AddScoped<IUserGroupService, UserGroupService>();
            services.AddScoped<IUserMessageService, UserMessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserPersonTaskCalendarFileService, UserPersonTaskCalendarFileService>();
            services.AddScoped<IWebApplicationService, WebApplicationService>();
            services.AddTransient<IAuditBll, AuditBll>();
           
            #endregion

            #region Customer

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IPersonAddressService, PersonAddressService>();
            services.AddScoped<IPersonAlertService, PersonAlertService>();
            services.AddScoped<IPersonAlertCommentService, PersonAlertCommentService>();
            services.AddScoped<IPersonCaseService, PersonCaseService>();
            services.AddScoped<IPersonAntiSocialBehaviourService, PersonAntiSocialBehaviourService>();
            services.AddScoped<IPersonAntiSocialBehaviourCaseNoteService, PersonAntiSocialBehaviourCaseNoteService>();
            services.AddScoped<IPersonDocumentService, PersonDocumentService>();
            services.AddScoped<IPropertyDocumentService, PropertyDocumentService>();
            services.AddScoped<IPersonApplicationLinkService, PersonApplicationLinkService>();
            services.AddScoped<IPersonContactDetailService, PersonContactDetailService>();
            services.AddScoped<IPersonFlagService, PersonFlagService>();
            services.AddScoped<IPersonFlagCommentService, PersonFlagCommentService>();
            services.AddScoped<ITenantService, TenantService>();
            #endregion

            #region Lookups
            services.AddTransient<IAlertGroupService, AlertGroupService>();
            services.AddTransient<IAlertTypeService, AlertTypeService>();
            services.AddTransient<IAlertStatusService, AlertStatusService>();
            services.AddTransient<IAntiSocialBehaviourCaseClosureReasonService, AntiSocialBehaviourCaseClosureReasonService>();
            services.AddTransient<IAntiSocialBehaviourCaseStatusService, AntiSocialBehaviourCaseStatusService>();
            services.AddTransient<IAntiSocialBehaviourTypeService, AntiSocialBehaviourTypeService>();

            services.AddTransient<IAddressTypeService, AddressTypeService>();
            services.AddTransient<IPersonCaseTypeService, PersonCaseTypeService>();
            services.AddTransient<IPersonCaseStatusService, PersonCaseStatusService>();
            services.AddTransient<IContactTypeService, ContactTypeService>();
            services.AddTransient<IDocumentTypeService, DocumentTypeService>();
            services.AddTransient<IEthnicityService, EthnicityService>();
            services.AddTransient<IFlagGroupService, FlagGroupService>();
            services.AddTransient<IFlagTypeService, FlagTypeService>();
            services.AddTransient<IGenderService, GenderService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IRelationshipService, RelationshipService>();
            services.AddTransient<ILookupBll, LookupBll>();
            services.AddTransient<ITitleService, TitleService>();
            services.AddTransient<INationalityTypeService, NationalityTypeService>();
            services.AddTransient<IPriorityService, PriorityService>();
            services.AddTransient<ITaskNotificationTypeService, TaskNotificationTypeService>();
            services.AddTransient<ITaskReminderTypeService, TaskReminderTypeService>();
            services.AddTransient<ITaskStatusService, TaskStatusService>();
            services.AddTransient<ITaskTypeService, TaskTypeService>();
            #endregion
            
            #region OdataService
            services.AddTransient<IOdataTenantService, OdataTenantService>();
            #endregion

            services.AddScoped<IRepository, Repository<CRMContext>>();


        }
    }
}

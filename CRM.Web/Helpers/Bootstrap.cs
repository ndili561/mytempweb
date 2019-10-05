using CRM.Web.Service;
using CRM.WebApiClient.HttpClient;
using CRM.WebApiClient.HttpClient.Common;
using CRM.WebApiClient.HttpClient.Customer;
using CRM.WebApiClient.HttpClient.Employee;
using CRM.WebApiClient.HttpClient.Facade;
using CRM.WebApiClient.HttpClient.Lookup;
using CRM.WebApiClient.Interface.Common;
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Employee;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Web.Helpers
{
    public static class Bootstrap
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            InitializeGatewayAPIClients(services);
            // InitializeAuthentication(services, configuration);
            // InitializeServices(services);
        }
        private static void InitializeGatewayAPIClients(IServiceCollection services)
        {
            #region Common
            services.AddTransient<IDocumentApiClient, DocumentApiClient>();
            services.AddTransient<IEmailApiClient, EmailApiClient>();
            services.AddTransient<ISmsApiClient, SmsApiClient>();
            services.AddTransient<IPropertyDetailViewApiClient, PropertyDetailViewApiClient>();
            services.AddTransient<IPropertyApiClient, PropertyApiClient>();
            #endregion

            #region Customer

            services.AddTransient<IAddressApiClient, AddressApiClient>();
            services.AddTransient<IPersonApiClient, PersonApiClient>();
            services.AddTransient<IPersonAddressApiClient, PersonAddressApiClient>();
            services.AddTransient<IPersonAlertApiClient, PersonAlertApiClient>();
            services.AddTransient<IPersonAlertCommentApiClient, PersonAlertCommentApiClient>();
            services.AddTransient<IPersonAntiSocialBehaviourApiClient, PersonAntiSocialBehaviourApiClient>();
            services.AddTransient<IPersonAntiSocialBehaviourCaseNoteApiClient, PersonAntiSocialBehaviourCaseNoteApiClient>();
            services.AddTransient<IPersonCaseApiClient, PersonCaseApiClient>();
            services.AddTransient<IPersonFlagApiClient, PersonFlagApiClient>();
            services.AddTransient<IPersonFlagCommentApiClient, PersonFlagCommentApiClient>();
            services.AddTransient<IPersonApplicationLinkApiClient, PersonApplicationLinkApiClient>();
            services.AddTransient<IPersonEmailApiClient, PersonEmailApiClient>();
            services.AddTransient<IPersonDocumentApiClient, PersonDocumentApiClient>();
            services.AddTransient<IPersonSmsApiClient, PersonSmsApiClient>();
            services.AddTransient<ITenantApiClient, TenantApiClient>();
            services.AddTransient<ITenantHistoryViewApiClient, TenantHistoryViewApiClient>();
            services.AddTransient<IPropertyVoidApiClient, PropertyVoidApiClient>();
            services.AddTransient<IPropertyDocumentApiClient, PropertyDocumentApiClient>();
            services.AddTransient<IRepairApiClient, RepairApiClient>();
            services.AddTransient<IVblApplicationApiClient, VblApplicationApiClient>();
            #endregion

            #region Lookup
            services.AddTransient<IAntiSocialBehaviourCaseClosureReasonApiClient, AntiSocialBehaviourCaseClosureReasonApiClient>();
            services.AddTransient<IAntiSocialBehaviourCaseStatusApiClient, AntiSocialBehaviourCaseStatusApiClient>();
            services.AddTransient<IAntiSocialBehaviourTypeApiClient, AntiSocialBehaviourTypeApiClient>();

            services.AddTransient<IAddressTypeApiClient, AddressTypeApiClient>();
            services.AddTransient<IAlertGroupApiClient, AlertGroupApiClient>();
            services.AddTransient<IAlertStatusApiClient, AlertStatusApiClient>();
            services.AddTransient<IAlertTypeApiClient, AlertTypeApiClient>();
            services.AddTransient<IPersonCaseStatusApiClient, PersonCaseStatusApiClient>();
            services.AddTransient<IPersonCaseTypeApiClient, PersonCaseTypeApiClient>();
            services.AddTransient<IDocumentTypeApiClient, DocumentTypeApiClient>();
            services.AddTransient<IEthnicityApiClient, EthnicityApiClient>();
            services.AddTransient<IFlagGroupApiClient, FlagGroupApiClient>();
            services.AddTransient<IFlagTypeApiClient, FlagTypeApiClient>();
            services.AddTransient<IGenderApiClient, GenderApiClient>();
            services.AddTransient<ILanguageApiClient, LanguageApiClient>();
            services.AddTransient<ILookupApiClient, LookupApiClient>();
            services.AddTransient<INationalityTypeApiClient, NationalityTypeApiClient>();
            services.AddTransient<IPriorityApiClient, PriorityApiClient>();
            services.AddTransient <IRelationshipApiClient,RelationshipApiClient>();
            services.AddTransient<ITaskNotificationTypeApiClient, TaskNotificationTypeApiClient>();
            services.AddTransient<ITaskReminderTypeApiClient, TaskReminderTypeApiClient>();
            services.AddTransient<ITaskStatusApiClient, TaskStatusApiClient>();
            services.AddTransient<ITitleApiClient, TitleApiClient>();
        
            #endregion

            #region User
          
            services.AddTransient<IApplicationRoleApiClient, ApplicationRoleApiClient>();
            services.AddTransient<IPermissionApiClient, PermissionApiClient>();
            services.AddTransient<IApplicationTaskApiClient, ApplicationTaskApiClient>();
            services.AddTransient<IAuditApiClient, AuditApiClient>();
            services.AddTransient<ITaskApiClient, TaskApiClient>();
            services.AddTransient<IChatMessageApiClient, ChatMessageApiClient>();
            services.AddTransient<IMenuItemApiClient, MenuItemApiClient>();
            services.AddTransient<IMessageApiClient, MessageApiClient>();
            services.AddTransient<IRoleApiClient, RoleApiClient>();
            services.AddTransient<ITaskTypeApiClient, TaskTypeApiClient>();
            services.AddTransient<IUserActivityApiClient, UserActivityApiClient>();
            services.AddTransient<IUserApplicationRoleApiClient, UserApplicationRoleApiClient>();
            services.AddTransient<IUserPersonTaskApiClient, UserPersonTaskApiClient>();
            services.AddTransient<IUserTaskNotificationApiClient, UserTaskNotificationApiClient>();
            services.AddTransient<IUserTaskReminderApiClient, UserTaskReminderApiClient>();
            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<IUserDiaryApiClient, UserDiaryApiClient>();
            services.AddTransient<IUserGroupApiClient, UserGroupApiClient>();
            services.AddTransient<IUserMessageApiClient, UserMessageApiClient>();
            services.AddTransient<IUserEmailApiClient, UserEmailApiClient>();
            services.AddTransient<IUserDocumentApiClient, UserDocumentApiClient>();
            services.AddTransient<IUserSmsApiClient, UserSmsApiClient>();
            services.AddTransient<IWebApplicationApiClient, WebApplicationApiClient>();

            #endregion

            #region Facade
            services.AddTransient<ICommonFacadeApiClient, CommonFacadeApiClient>();
            services.AddTransient<ICustomerFacadeApiClient, CustomerFacadeApiClient>();
            services.AddTransient<IEmployeeFacadeApiClient, EmployeeFacadeApiClient>();
            services.AddTransient<ILookupFacadeApiClient, LookupFacadeApiClient>();
            services.AddTransient<IGatewayFacadeApiClient, GatewayFacadeApiClient>();
            services.AddTransient<IPropertyFacadeApiClient, PropertyFacadeApiClient>();
            #endregion
        }

        private static void InitializeServices(IServiceCollection services)
        {
            services.AddTransient<ILoginUserService, LoginUserService>();
        }
        //private static void InitializeAuthentication(IServiceCollection services, IConfiguration configuration)
        //{
        //    var identitySettings = configuration.GetSection("IdentitySettings").Get<IdentitySettings>();

        //    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        //    services.AddAuthentication(options =>
        //        {
        //            options.DefaultScheme = "FieldManagerCookies";
        //            options.DefaultChallengeScheme = "FieldManageroidc";
        //        })
        //        .AddCookie("FieldManagerCookies", options =>
        //        {
        //            options.AccessDeniedPath = "/Home/ErrorForbidden";
        //            options.LoginPath = "/Home/ErrorNotLoggedIn";
        //        })
        //        .AddOpenIdConnect("FieldManageroidc", options =>
        //        {
        //            options.Authority = identitySettings.Authority;
        //            options.ClientId = identitySettings.ClientId;
        //            options.ClientSecret = identitySettings.ClientSecret.ToSha256();
        //            options.ResponseType = "id_token token";
        //            options.SignedOutRedirectUri = identitySettings.SignedOutRedirectUri;
        //            options.SaveTokens = true;
        //            options.GetClaimsFromUserInfoEndpoint = true;
        //            options.Scope.Clear();
        //            options.Scope.Add("openid profile roles all_claims");
        //            options.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                NameClaimType = JwtClaimTypes.Name,
        //                RoleClaimType = JwtClaimTypes.Role
        //            };
        //        })
        //        ;
        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy("FieldManagerAdmin", p => p.RequireAuthenticatedUser().RequireRole("FieldManager Admin"));
        //    });
        //}

    }
}

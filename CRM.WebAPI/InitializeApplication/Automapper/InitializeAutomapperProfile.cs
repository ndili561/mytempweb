using AutoMapper;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Common;
using CRM.Dto.Customer;
using CRM.Dto.Employee;
using CRM.Dto.Lookup;

namespace CRM.WebAPI.InitializeApplication.Automapper
{
    public class InitializeAutomapperProfile : Profile
    {
        public InitializeAutomapperProfile()
        {
            #region Common
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<EmailLabel, EmailLabelDto>().ReverseMap();
            CreateMap<Sms, SmsDto>().ReverseMap();
            #endregion

            #region Customer

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<PersonAlert, PersonAlertDto>().ReverseMap();
            CreateMap<PersonAlertComment, PersonAlertCommentDto>().ReverseMap();
            CreateMap<PersonCase, PersonCaseDto>().ReverseMap();
            CreateMap<PersonAddress, PersonAddressDto>().ReverseMap();
            CreateMap<PersonApplicationLink, PersonApplicationLinkDto>().ReverseMap();
            CreateMap<PersonAntiSocialBehaviour, PersonAntiSocialBehaviourDto>().ReverseMap();
            CreateMap<PersonAntiSocialBehaviourCaseNote, PersonAntiSocialBehaviourCaseNoteDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<PersonDocument, PersonDocumentDto>().ReverseMap();
            CreateMap<PersonFlag, PersonFlagDto>().ReverseMap();
            CreateMap<PersonFlagComment, PersonFlagCommentDto>().ReverseMap();
            CreateMap<MergePerson, MergePersonDto>().ReverseMap();
            CreateMap<PersonContactDetail, PersonContactDetailDto>().ReverseMap();
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<PropertyDetailView, PropertyDetailViewDto>().ReverseMap();
            CreateMap<PropertyDocument, PropertyDocumentDto>().ReverseMap();
            CreateMap<PropertyVoid, PropertyVoidDto>().ReverseMap();
            CreateMap<PropertyVoidView, PropertyVoidViewDto>().ReverseMap();
            CreateMap<Tenant, TenantDto>().ReverseMap();
            #endregion

            #region Employee

            CreateMap<Audit, AuditDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<ApplicationRole, ApplicationRoleDto>().ReverseMap();
            CreateMap<UserApplicationRole, UserApplicationRoleDto>().ReverseMap();

            CreateMap<Task, TaskDto>().ReverseMap();
            CreateMap<ChatMessage, ChatMessageDto>().ReverseMap();
            CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
           
            CreateMap<UserActivity, UserActivityDto>().ReverseMap();
           
            CreateMap<UserPersonTask, UserPersonTaskDto>().ReverseMap();
            CreateMap<UserTaskNotification, UserTaskNotificationDto>().ReverseMap();
            CreateMap<UserTaskReminder, UserTaskReminderDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDiary, UserDiaryDto>().ReverseMap();
            CreateMap<UserGroup, UserGroupDto>().ReverseMap();
            CreateMap<UserMessage, UserMessageDto>().ReverseMap();
            CreateMap<WebApplication, WebApplicationDto>().ReverseMap();
            #endregion

            #region Lookup

            CreateMap<BaseLookup, BaseLookupDto>().ReverseMap();
            CreateMap<ContactType, ContactTypeDto>().ReverseMap();
            CreateMap<AddressType, AddressTypeDto>().ReverseMap();
            CreateMap<AlertType, AlertTypeDto>().ReverseMap();
            CreateMap<AlertGroup, AlertGroupDto>().ReverseMap();
            CreateMap<AlertStatus, AlertStatusDto>().ReverseMap();
            CreateMap<AntiSocialBehaviourCaseStatus, AntiSocialBehaviourCaseStatusDto>().ReverseMap();
            CreateMap<AntiSocialBehaviourType, AntiSocialBehaviourTypeDto>().ReverseMap();
            CreateMap<AntiSocialBehaviourCaseClosureReason, AntiSocialBehaviourCaseClosureReasonDto>().ReverseMap();
            CreateMap<PersonCaseType, PersonCaseTypeDto>().ReverseMap();
            CreateMap<PersonCaseStatus, PersonCaseStatusDto>().ReverseMap();
            CreateMap<ContactByOption, ContactByOptionDto>().ReverseMap();
            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
            CreateMap<EmailCategory, EmailCategoryDto>().ReverseMap();
            CreateMap<EmailLabelType, EmailLabelTypeDto>().ReverseMap();
            CreateMap<EmailStatus, EmailStatusDto>().ReverseMap();
            CreateMap<Ethnicity, EthnicityDto>().ReverseMap();
            CreateMap<FlagType, FlagTypeDto>().ReverseMap();
            CreateMap<FlagGroup, FlagGroupDto>().ReverseMap();
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<Relationship, RelationshipDto>().ReverseMap();
            CreateMap<NationalityType, NationalityTypeDto>().ReverseMap();
            CreateMap<Priority, PriorityDto>().ReverseMap();
            CreateMap<TaskNotificationType, TaskNotificationTypeDto>().ReverseMap();
            CreateMap<TaskReminderType, TaskReminderTypeDto>().ReverseMap();
            CreateMap<TaskStatus, TaskStatusDto>().ReverseMap();
            CreateMap<Title, TitleDto>().ReverseMap();

            #endregion

            //Mapper.Initialize(cfg =>
            //    cfg.CreateMap<User, UserDto>().ReverseMap());

            //Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}

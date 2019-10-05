using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;
using CRM.WebApiClient.Interface.Facade;
using CRM.WebApiClient.Interface.Lookup;

namespace CRM.WebApiClient.HttpClient.Facade
{
    public class LookupFacadeApiClient : ILookupFacadeApiClient
    {

        private readonly IAddressTypeApiClient _addressTypeApiClient;
        private readonly IAlertTypeApiClient _alertTypeApiClient;
        private readonly IAlertGroupApiClient _alertGroupApiClient;
        private readonly IAlertStatusApiClient _alertStatusApiClient;

        private readonly IAntiSocialBehaviourCaseClosureReasonApiClient _antiSocialBehaviourCaseClosureReasonApiClient;
        private readonly IAntiSocialBehaviourCaseStatusApiClient _antiSocialBehaviourCaseStatusApiClient;
        private readonly IAntiSocialBehaviourTypeApiClient _antiSocialBehaviourTypeApiClient;
        private readonly IPersonCaseTypeApiClient _caseTypeApiClient;
        private readonly IPersonCaseStatusApiClient _caseStatusApiClient;
        private readonly IDocumentTypeApiClient _documentTypeApiClient;
        private readonly IEthnicityApiClient _ethnicityApiClient;
        private readonly IFlagTypeApiClient _flagTypeApiClient;
        private readonly IFlagGroupApiClient _flagGroupApiClient;
        private readonly IGenderApiClient _genderApiClient;
        private readonly ILanguageApiClient _languageApiClient;
        private readonly IRelationshipApiClient _relationshipApiClient;
        private readonly INationalityTypeApiClient _nationalityTypeApiClient;
        private readonly IPriorityApiClient _priorityApiClient;
        private readonly ITaskNotificationTypeApiClient _taskNotificationTypeApiClient;
        private readonly ITaskReminderTypeApiClient _taskReminderTypeApiClient;
        private readonly ITaskStatusApiClient _taskStatusApiClient;
        private readonly ITitleApiClient _titleApiClient;

        public LookupFacadeApiClient(
            IAddressTypeApiClient addressTypeApiClient,
            IAlertGroupApiClient alertGroupApiClient,
            IAlertTypeApiClient alertTypeApiClient,
            IAlertStatusApiClient alertStatusApiClient,
            IAntiSocialBehaviourCaseClosureReasonApiClient antiSocialBehaviourCaseClosureReasonApiClient,
            IAntiSocialBehaviourCaseStatusApiClient antiSocialBehaviourCaseStatusApiClient,
            IAntiSocialBehaviourTypeApiClient antiSocialBehaviourTypeApiClient,
            IPersonCaseTypeApiClient caseTypeApiClient,
            IPersonCaseStatusApiClient caseStatusApiClient,
            IDocumentTypeApiClient documentTypeApiClient,
            IEthnicityApiClient ethnicityApiClient,
            IFlagTypeApiClient flagTypeApiClient,
            IFlagGroupApiClient flagGroupApiClient,
            IGenderApiClient genderApiClient,
            ILanguageApiClient languageApiClient,
            IRelationshipApiClient relationshipApiClient,
            INationalityTypeApiClient nationalityTypeApiClient,
            IPriorityApiClient priorityApiClient,
            ITaskNotificationTypeApiClient taskNotificationTypeApiClient,
            ITaskReminderTypeApiClient taskReminderTypeApiClient,
            ITaskStatusApiClient taskStatusApiClient,
            ITitleApiClient titleApiClient
            )
        {
            _addressTypeApiClient = addressTypeApiClient;
            _alertGroupApiClient = alertGroupApiClient;
            _alertTypeApiClient = alertTypeApiClient;
            _alertStatusApiClient = alertStatusApiClient;
            _antiSocialBehaviourCaseClosureReasonApiClient = antiSocialBehaviourCaseClosureReasonApiClient;
            _antiSocialBehaviourCaseStatusApiClient = antiSocialBehaviourCaseStatusApiClient;
            _antiSocialBehaviourTypeApiClient = antiSocialBehaviourTypeApiClient;
            _caseTypeApiClient = caseTypeApiClient;
            _caseStatusApiClient = caseStatusApiClient;
            _documentTypeApiClient = documentTypeApiClient;
            _ethnicityApiClient = ethnicityApiClient;
            _flagTypeApiClient = flagTypeApiClient;
            _flagGroupApiClient = flagGroupApiClient;
            _genderApiClient = genderApiClient;
            _languageApiClient = languageApiClient;
            _relationshipApiClient = relationshipApiClient;
            _nationalityTypeApiClient = nationalityTypeApiClient;
            _priorityApiClient = priorityApiClient;
            _taskNotificationTypeApiClient = taskNotificationTypeApiClient;
            _taskReminderTypeApiClient = taskReminderTypeApiClient;
            _taskStatusApiClient = taskStatusApiClient;
            _titleApiClient = titleApiClient;
        }


        public async Task<AddressTypeSearchModel> GetAddressTypes(AddressTypeSearchModel model)
        {
            return await _addressTypeApiClient.GetAddressTypes(model);
        }

        public async Task<AddressTypeDto> GetAddressType(int addressTypeId)
        {
            return await _addressTypeApiClient.GetAddressType(addressTypeId);
        }

        public async Task<AddressTypeDto> PostAddressType(AddressTypeDto model)
        {
            return await _addressTypeApiClient.PostAddressType(model);
        }

        public async Task<AddressTypeDto> PutAddressType(int id, AddressTypeDto model)
        {
            return await _addressTypeApiClient.PutAddressType(id, model);
        }

        public async Task<EthnicitySearchModel> GetEthnicities(EthnicitySearchModel model)
        {
            return await _ethnicityApiClient.GetEthnicities(model);
        }

        public async Task<EthnicityDto> GetEthnicity(int ethnicityId)
        {
            return await _ethnicityApiClient.GetEthnicity(ethnicityId);
        }

        public async Task<EthnicityDto> PostEthnicity(EthnicityDto model)
        {
            return await _ethnicityApiClient.PostEthnicity(model);
        }

        public async Task<EthnicityDto> PutEthnicity(int id, EthnicityDto model)
        {
            return await _ethnicityApiClient.PutEthnicity(id, model);
        }

        public async Task<GenderSearchModel> GetGenders(GenderSearchModel model)
        {
            return await _genderApiClient.GetGenders(model);
        }

        public async Task<GenderDto> GetGender(int genderId)
        {
            return await _genderApiClient.GetGender(genderId);
        }

        public async Task<GenderDto> PostGender(GenderDto model)
        {
            return await _genderApiClient.PostGender(model);
        }

        public async Task<GenderDto> PutGender(int id, GenderDto model)
        {
            return await _genderApiClient.PutGender(id, model);
        }

        public async Task<LanguageSearchModel> GetLanguages(LanguageSearchModel model)
        {
            return await _languageApiClient.GetLanguages(model);
        }

        public async Task<LanguageDto> GetLanguage(int languageId)
        {
            return await _languageApiClient.GetLanguage(languageId);
        }

        public async Task<LanguageDto> PostLanguage(LanguageDto model)
        {
            return await _languageApiClient.PostLanguage(model);
        }

        public async Task<LanguageDto> PutLanguage(int id, LanguageDto model)
        {
            return await _languageApiClient.PutLanguage(id, model);
        }

        public async Task<NationalityTypeSearchModel> GetNationalityTypes(NationalityTypeSearchModel model)
        {
            return await _nationalityTypeApiClient.GetNationalityTypes(model);
        }

        public async Task<NationalityTypeDto> GetNationalityType(int nationalityTypeId)
        {
            return await _nationalityTypeApiClient.GetNationalityType(nationalityTypeId);
        }

        public async Task<NationalityTypeDto> PostNationalityType(NationalityTypeDto model)
        {
            return await _nationalityTypeApiClient.PostNationalityType(model);
        }

        public async Task<NationalityTypeDto> PutNationalityType(int id, NationalityTypeDto model)
        {
            return await _nationalityTypeApiClient.PutNationalityType(id, model);
        }

        public async Task<TaskNotificationTypeSearchModel> GetTaskNotificationTypes(TaskNotificationTypeSearchModel model)
        {
            return await _taskNotificationTypeApiClient.GetTaskNotificationTypes(model);
        }

        public async Task<TaskNotificationTypeDto> GetTaskNotificationType(int id)
        {
            return await _taskNotificationTypeApiClient.GetTaskNotificationType(id);
        }

        public async Task<TaskNotificationTypeDto> PostTaskNotificationType(TaskNotificationTypeDto model)
        {
            return await _taskNotificationTypeApiClient.PostTaskNotificationType(model);
        }

        public async Task<TaskNotificationTypeDto> PutTaskNotificationType(int id, TaskNotificationTypeDto model)
        {
            return await _taskNotificationTypeApiClient.PutTaskNotificationType(id, model);
        }

        public async Task<TaskReminderTypeSearchModel> GetTaskReminderTypes(TaskReminderTypeSearchModel model)
        {
            return await _taskReminderTypeApiClient.GetTaskReminderTypes(model);
        }

        public async Task<TaskReminderTypeDto> GetTaskReminderType(int id)
        {
            return await _taskReminderTypeApiClient.GetTaskReminderType(id);
        }

        public async Task<TaskReminderTypeDto> PostTaskReminderType(TaskReminderTypeDto model)
        {
            return await _taskReminderTypeApiClient.PostTaskReminderType(model);
        }

        public async Task<TaskReminderTypeDto> PutTaskReminderType(int id, TaskReminderTypeDto model)
        {
            return await _taskReminderTypeApiClient.PutTaskReminderType(id, model);
        }

        public async Task<TaskStatusSearchModel> GetTaskStatuses(TaskStatusSearchModel model)
        {
            return await _taskStatusApiClient.GetTaskStatuses(model);
        }

        public async Task<TaskStatusDto> GetTaskStatus(int taskStatusId)
        {
            return await _taskStatusApiClient.GetTaskStatus(taskStatusId);
        }

        public async Task<TaskStatusDto> PostTaskStatus(TaskStatusDto model)
        {
            return await _taskStatusApiClient.PostTaskStatus(model);
        }

        public async Task<TaskStatusDto> PutTaskStatus(int id, TaskStatusDto model)
        {
            return await _taskStatusApiClient.PutTaskStatus(id, model);
        }

        public async Task<TitleSearchModel> GetTitles(TitleSearchModel model)
        {
            return await _titleApiClient.GetTitles(model);
        }

        public async Task<TitleDto> GetTitle(int titleId)
        {
            return await _titleApiClient.GetTitle(titleId);
        }

        public async Task<TitleDto> PostTitle(TitleDto model)
        {
            return await _titleApiClient.PostTitle(model);
        }

        public async Task<TitleDto> PutTitle(int id, TitleDto model)
        {
            return await _titleApiClient.PutTitle(id, model);
        }

        public async Task<DocumentTypeSearchModel> GetDocumentTypes(DocumentTypeSearchModel model)
        {
            return await _documentTypeApiClient.GetDocumentTypes(model);
        }

        public async Task<DocumentTypeDto> GetDocumentType(int id)
        {
            return await _documentTypeApiClient.GetDocumentType(id);
        }

        public async Task<DocumentTypeDto> PostDocumentType(DocumentTypeDto model)
        {
            return await _documentTypeApiClient.PostDocumentType(model);
        }

        public async Task<DocumentTypeDto> PutDocumentType(int id, DocumentTypeDto model)
        {
            return await _documentTypeApiClient.PutDocumentType(id, model);
        }

        public async Task<RelationshipSearchModel> GetRelationships(RelationshipSearchModel model)
        {
            return await _relationshipApiClient.GetRelationships(model);
        }

        public async Task<RelationshipDto> GetRelationship(int id)
        {
            return await _relationshipApiClient.GetRelationship(id);
        }

        public async Task<RelationshipDto> PostRelationship(RelationshipDto model)
        {
            return await _relationshipApiClient.PostRelationship(model);
        }

        public async Task<RelationshipDto> PutRelationship(int id, RelationshipDto model)
        {
            return await _relationshipApiClient.PutRelationship(id, model);
        }

        public async Task<AlertTypeSearchModel> GetAlertTypes(AlertTypeSearchModel model)
        {
            return await _alertTypeApiClient.GetAlertTypes( model);
        }

        public async Task<AlertTypeDto> GetAlertType(int id)
        {
            return await _alertTypeApiClient.GetAlertType(id);
        }

        public async Task<AlertTypeDto> PostAlertType(AlertTypeDto model)
        {
            return await _alertTypeApiClient.PostAlertType(model);
        }

        public async Task<AlertTypeDto> PutAlertType(int id, AlertTypeDto model)
        {
            return await _alertTypeApiClient.PutAlertType(id,model);
        }

        public async Task<AlertStatusSearchModel> GetAlertStatuses(AlertStatusSearchModel model)
        {
            return await _alertStatusApiClient.GetAlertStatuses(model);
        }

        public async Task<AlertStatusDto> GetAlertStatus(int id)
        {
            return await _alertStatusApiClient.GetAlertStatus(id);
        }

        public async Task<AlertStatusDto> PostAlertStatus(AlertStatusDto model)
        {
            return await _alertStatusApiClient.PostAlertStatus(model);
        }

        public async Task<AlertStatusDto> PutAlertStatus(int id, AlertStatusDto model)
        {
            return await _alertStatusApiClient.PutAlertStatus(id,model);
        }

        public async Task<PersonCaseTypeSearchModel> GetCaseTypes(PersonCaseTypeSearchModel model)
        {
            return await _caseTypeApiClient.GetCaseTypes(model);
        }

        public async Task<PersonCaseTypeDto> GetCaseType(int id)
        {
            return await _caseTypeApiClient.GetCaseType(id);
        }

        public async Task<PersonCaseTypeDto> PostCaseType(PersonCaseTypeDto model)
        {
            return await _caseTypeApiClient.PostCaseType(model);
        }

        public async Task<PersonCaseTypeDto> PutCaseType(int id, PersonCaseTypeDto model)
        {
            return await _caseTypeApiClient.PutCaseType(id,model);
        }

        public async Task<PersonCaseStatusSearchModel> GetCaseStatuses(PersonCaseStatusSearchModel model)
        {
            return await _caseStatusApiClient.GetCaseStatuses( model);
        }

        public async Task<PersonCaseStatusDto> GetCaseStatus(int id)
        {
            return await _caseStatusApiClient.GetCaseStatus(id);
        }

        public async Task<PersonCaseStatusDto> PostCaseStatus(PersonCaseStatusDto model)
        {
            return await _caseStatusApiClient.PostCaseStatus( model);
        }

        public async Task<PersonCaseStatusDto> PutCaseStatus(int id, PersonCaseStatusDto model)
        {
            return await _caseStatusApiClient.PutCaseStatus(id, model);
        }

        public async Task<AlertGroupSearchModel> GetAlertGroups(AlertGroupSearchModel model)
        {
            return await _alertGroupApiClient.GetAlertGroups( model);
        }

        public async Task<AlertGroupDto> GetAlertGroup(int id)
        {
            return await _alertGroupApiClient.GetAlertGroup(id);
        }

        public async Task<AlertGroupDto> PostAlertGroup(AlertGroupDto model)
        {
            return await _alertGroupApiClient.PostAlertGroup(model);
        }

        public async Task<AlertGroupDto> PutAlertGroup(int id, AlertGroupDto model)
        {
            return await _alertGroupApiClient.PutAlertGroup(id,model);
        }

        public async Task<FlagTypeSearchModel> GetFlagTypes(FlagTypeSearchModel model)
        {
            return await _flagTypeApiClient.GetFlagTypes(model);
        }

        public async Task<FlagTypeDto> GetFlagType(int id)
        {
            return await _flagTypeApiClient.GetFlagType(id);
        }

        public async Task<FlagTypeDto> PostFlagType(FlagTypeDto model)
        {
            return await _flagTypeApiClient.PostFlagType(model);
        }

        public async Task<FlagTypeDto> PutFlagType(int id, FlagTypeDto model)
        {
            return await _flagTypeApiClient.PutFlagType(id, model);
        }

        public async Task<FlagGroupSearchModel> GetFlagGroups(FlagGroupSearchModel model)
        {
            return await _flagGroupApiClient.GetFlagGroups(model);
        }

        public async Task<FlagGroupDto> GetFlagGroup(int id)
        {
            return await _flagGroupApiClient.GetFlagGroup(id);
        }

        public async Task<FlagGroupDto> PostFlagGroup(FlagGroupDto model)
        {
            return await _flagGroupApiClient.PostFlagGroup(model);
        }

        public async Task<FlagGroupDto> PutFlagGroup(int id, FlagGroupDto model)
        {
            return await _flagGroupApiClient.PutFlagGroup(id, model);
        }

        public async Task<PrioritySearchModel> GetPriorities(PrioritySearchModel model)
        {
            return await _priorityApiClient.GetPriorities(model);
        }

        public async Task<PriorityDto> GetPriority(int id)
        {
            return await _priorityApiClient.GetPriority(id);
        }

        public async Task<PriorityDto> PostPriority(PriorityDto model)
        {
            return await _priorityApiClient.PostPriority(model);
        }

        public async Task<PriorityDto> PutPriority(int id, PriorityDto model)
        {
            return await _priorityApiClient.PutPriority(id,model);
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonSearchModel> GetAntiSocialBehaviourCaseClosureReasons(AntiSocialBehaviourCaseClosureReasonSearchModel model)
        {
            return await _antiSocialBehaviourCaseClosureReasonApiClient.GetAntiSocialBehaviourCaseClosureReasons( model);
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonDto> GetAntiSocialBehaviourCaseClosureReason(int id)
        {
            return await _antiSocialBehaviourCaseClosureReasonApiClient.GetAntiSocialBehaviourCaseClosureReason(id);
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonDto> PostAntiSocialBehaviourCaseClosureReason(AntiSocialBehaviourCaseClosureReasonDto model)
        {
            return await _antiSocialBehaviourCaseClosureReasonApiClient.PostAntiSocialBehaviourCaseClosureReason(model);
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonDto> PutAntiSocialBehaviourCaseClosureReason(int id, AntiSocialBehaviourCaseClosureReasonDto model)
        {
            return await _antiSocialBehaviourCaseClosureReasonApiClient.PutAntiSocialBehaviourCaseClosureReason(id,model);
        }

        public async Task<AntiSocialBehaviourCaseStatusSearchModel> GetAntiSocialBehaviourCaseStatuses(AntiSocialBehaviourCaseStatusSearchModel model)
        {
            return await _antiSocialBehaviourCaseStatusApiClient.GetAntiSocialBehaviourCaseStatuses(model);
        }

        public async Task<AntiSocialBehaviourCaseStatusDto> GetAntiSocialBehaviourCaseStatus(int id)
        {
            return await _antiSocialBehaviourCaseStatusApiClient.GetAntiSocialBehaviourCaseStatus(id);
        }

        public async Task<AntiSocialBehaviourCaseStatusDto> PostAntiSocialBehaviourCaseStatus(AntiSocialBehaviourCaseStatusDto model)
        {
            return await _antiSocialBehaviourCaseStatusApiClient.PostAntiSocialBehaviourCaseStatus(model);
        }

        public async Task<AntiSocialBehaviourCaseStatusDto> PutAntiSocialBehaviourCaseStatus(int id, AntiSocialBehaviourCaseStatusDto model)
        {
            return await _antiSocialBehaviourCaseStatusApiClient.PutAntiSocialBehaviourCaseStatus(id,model);
        }

        public async Task<AntiSocialBehaviourTypeSearchModel> GetAntiSocialBehaviourTypes(AntiSocialBehaviourTypeSearchModel model)
        {
            return await _antiSocialBehaviourTypeApiClient.GetAntiSocialBehaviourTypes(model);
        }

        public async Task<AntiSocialBehaviourTypeDto> GetAntiSocialBehaviourType(int id)
        {
            return await _antiSocialBehaviourTypeApiClient.GetAntiSocialBehaviourType(id);
        }

        public async Task<AntiSocialBehaviourTypeDto> PostAntiSocialBehaviourType(AntiSocialBehaviourTypeDto model)
        {
            return await _antiSocialBehaviourTypeApiClient.PostAntiSocialBehaviourType(model);
        }

        public async Task<AntiSocialBehaviourTypeDto> PutAntiSocialBehaviourType(int id, AntiSocialBehaviourTypeDto model)
        {
            return await _antiSocialBehaviourTypeApiClient.PutAntiSocialBehaviourType(id, model);
        }
    }
}
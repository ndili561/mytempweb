using System;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace CRM.WebAPI.ODataModelBuilder
{
    public class CRMModelBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        public CRMModelBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder(_serviceProvider);
            builder.EntitySet<Audit>(nameof(Audit))
                .EntityType.HasKey(s => s.Id)
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select()
                .Expand();

            InitializeOdata<ApplicationUser>(builder, nameof(ApplicationUser));
            InitializeOdata<User>(builder, nameof(User));
            //builder.EntitySet<User>(nameof(User))
            //    .EntityType.HasKey(s => s.Id)
            //    .HasMany(x => x.Applications);
            InitializeOdata<ApplicationRole>(builder, nameof(ApplicationRole));
            InitializeOdata<ApplicationUser>(builder, nameof(ApplicationUser));
            InitializeOdata<Permission>(builder, nameof(Permission));
            InitializeOdata<UserApplicationRole>(builder, nameof(UserApplicationRole));
            InitializeOdata<WebApplication>(builder, nameof(WebApplication));
            InitializeOdata<Role>(builder, nameof(Role));
            InitializeOdata<Person>(builder, nameof(Person));
            InitializeOdata<PersonAddress>(builder, nameof(PersonAddress));
            InitializeOdata<Address>(builder, nameof(Address));
            InitializeOdata<Document>(builder, nameof(Document));
            InitializeOdata<Email>(builder, nameof(Email));
            InitializeOdata<Sms>(builder, nameof(Sms));

            InitializeOdata<PersonDocument>(builder, nameof(PersonDocument));
            InitializeOdata<PersonEmail>(builder, nameof(PersonEmail));
            InitializeOdata<PersonSms>(builder, nameof(PersonSms));
            InitializeOdata<PersonAddress>(builder, nameof(PersonAddress));
            InitializeOdata<PersonAlert>(builder, nameof(PersonAlert));
            InitializeOdata<PersonAlertComment>(builder, nameof(PersonAlertComment));
            InitializeOdata<PersonCase>(builder, nameof(PersonCase));
            InitializeOdata<PersonFlag>(builder, nameof(PersonFlag));
            InitializeOdata<PersonFlagComment>(builder, nameof(PersonFlagComment));
            InitializeOdata<PersonAntiSocialBehaviour>(builder, nameof(PersonAntiSocialBehaviour));
            InitializeOdata<PersonAntiSocialBehaviourCaseNote>(builder, nameof(PersonAntiSocialBehaviourCaseNote));
            InitializeOdata<Property>(builder, nameof(Property));
            InitializeOdata<PropertyVoid>(builder, nameof(PropertyVoid));
            InitializeOdata<PropertyDocument>(builder, nameof(PropertyDocument));
            InitializeOdata<Tenant>(builder, nameof(Tenant));

            InitializeOdata<UserDocument>(builder, nameof(UserDocument));
            InitializeOdata<UserEmail>(builder, nameof(UserEmail));
            InitializeOdata<UserSms>(builder, nameof(UserSms));
            InitializeOdata<UserPersonTask>(builder, nameof(UserPersonTask));
            InitializeOdata<User>(builder, nameof(User));

            InitializeOdata<AddressType>(builder, nameof(AddressType));
            InitializeOdata<AlertStatus>(builder, nameof(AlertStatus));
            InitializeOdata<AlertType>(builder, nameof(AlertType));
            InitializeOdata<AlertGroup>(builder, nameof(AlertGroup));
            InitializeOdata<AntiSocialBehaviourType>(builder, nameof(AntiSocialBehaviourType));
            InitializeOdata<AntiSocialBehaviourCaseStatus>(builder, nameof(AntiSocialBehaviourCaseStatus));
            InitializeOdata<AntiSocialBehaviourCaseClosureReason>(builder, nameof(AntiSocialBehaviourCaseClosureReason));

            InitializeOdata<PersonCaseStatus>(builder, nameof(PersonCaseStatus));
            InitializeOdata<PersonCaseType>(builder, nameof(PersonCaseType));
            InitializeOdata<ContactByOption>(builder, nameof(ContactByOption));
            InitializeOdata<ContactType>(builder, nameof(ContactType));
            InitializeOdata<Gender>(builder, nameof(Gender));
            InitializeOdata<Ethnicity>(builder, nameof(Ethnicity));
            InitializeOdata<FlagType>(builder, nameof(FlagType));
            InitializeOdata<FlagGroup>(builder, nameof(FlagGroup));
            InitializeOdata<Language>(builder, nameof(Language));
            InitializeOdata<NationalityType>(builder, nameof(NationalityType));
            InitializeOdata<Priority>(builder, nameof(Priority));
            InitializeOdata<Relationship>(builder, nameof(Relationship));
            InitializeOdata<Title>(builder, nameof(Title));
            InitializeOdata<Lookup>(builder, nameof(Lookup));
            InitializeOdata<ImageGroup>(builder, nameof(ImageGroup));

            InitializeOdata<ApplicationTask>(builder, nameof(ApplicationTask));
            InitializeOdata<Task>(builder, nameof(Task));
            InitializeOdata<TaskType>(builder, nameof(TaskType));

            // View doen not inherit from the base class BaseEntity
            builder.EntitySet<PropertyVoidView>(nameof(PropertyVoidView))
                .EntityType.HasKey(s => s.VoidId)
                .Filter().Count().Expand().OrderBy().Page().Select().Expand();
            builder.EntitySet<PropertyDetailView>(nameof(PropertyDetailView))
                .EntityType.HasKey(s => s.PropertyCode)
                .Filter().Count().Expand().OrderBy().Page().Select().Expand();
            builder.EntitySet<TenantHistoryView>(nameof(TenantHistoryView))
                .EntityType
                .HasKey(s => s.TenancyPersonReference)
                .HasKey(s => s.TenantCode)
                .HasKey(s => s.IsMainTenant)
                .Filter().Count().Expand().OrderBy().Page().Select().Expand();

            return builder.GetEdmModel();
        }

        private void InitializeOdata<T>(ODataConventionModelBuilder builder, string name) where T : BaseEntity
        {
            builder.EntitySet<T>(name)
                .EntityType.HasKey(s => s.Id)
                .Filter() // Allow for the $filter Command
                .Count() // Allow for the $count Command
                .Expand() // Allow for the $expand Command
                .OrderBy() // Allow for the $orderby Command
                .Page() // Allow for the $top and $skip Commands
                .Select() // Allow for the $select Command
                .Expand();
        }
    }
}

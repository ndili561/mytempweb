using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;
using Microsoft.EntityFrameworkCore;
using Task = CRM.DAL.Database.Entities.Employee.Task;
using TaskStatus = CRM.DAL.Database.Entities.Lookup.TaskStatus;

namespace CRM.DAL.Database
{
    public abstract class CoreDbContext : DbContext
    {
        public bool RecordCustomAudit { get; set; }//Property Dependency Injection
        public string CurrentLoggedUserId { get; set; }//Property Dependency Injection
        protected User CurrentLoggedUser { get; set; }//Property Dependency Injection
        protected CoreDbContext(DbContextOptions<CRMContext> options) : base(options)
        {

        }
    }
    public class CRMContext : CoreDbContext
    {

        public CRMContext(DbContextOptions<CRMContext> options) : base(options)
        {
            DbInitializer.Initialize(this);
        }

        #region Views
        public DbSet<IdentityUserView> IdentityUserView { get; set; }
        public DbSet<TenantHistoryView> TenantHistoryView { get; set; }
        public DbSet<PropertyDetailView> PropertyDetailView { get; set; }
        public DbSet<PropertyVoidView> PropertyVoidView { get; set; }

        #endregion
        public DbSet<Address> Addresses { get; set; }

        public DbSet<WebApplication> Applications { get; set; }
        public DbSet<ApplicationTask> ApplicationTasks { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailStatus> EmailStatuses { get; set; }
        public DbSet<EmailCategory> EmailCategories { get; set; }
        public DbSet<EmailLabel> EmailLabels { get; set; }
        public DbSet<EmailLabelType> EmailLabelTypes { get; set; }

        #region Lookup
        public DbSet<AntiSocialBehaviourCaseStatus> AntiSocialBehaviourCaseStatuses { get; set; }
        public DbSet<AntiSocialBehaviourCaseClosureReason> AntiSocialBehaviourCaseClosureReasons { get; set; }
        public DbSet<AntiSocialBehaviourType> AntiSocialBehaviourTypes { get; set; }
        public DbSet<AlertType> AlertTypes { get; set; }
        public DbSet<AlertGroup> AlertGroups { get; set; }
        public DbSet<AlertStatus> AlertStatuses { get; set; }
        public DbSet<FlagType> FlagTypes { get; set; }
        public DbSet<FlagGroup> FlagGroups { get; set; }

        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<ApplicantType> ApplicantTypes { get; set; }
        public DbSet<ContactByOption> ContactByOptions { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<ImageGroup> ImageGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<NationalityType> NationalityTypes { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<PersonCaseType> PersonCaseTypes { get; set; }
        public DbSet<PersonCaseStatus> PersonCaseStatuses { get; set; }
        #endregion
        public DbSet<PersonAlert> PersonAlerts { get; set; }
        public DbSet<PersonAlertComment> PersonAlertComments { get; set; }
        public DbSet<PersonCase> PersonCases { get; set; }
        public DbSet<PersonAntiSocialBehaviour> PersonAntiSocialBehaviours { get; set; }
        public DbSet<PersonAntiSocialBehaviourCaseNote> PersonAntiSocialBehaviourCaseNotes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonAddress> PersonAddresses { get; set; }
        public DbSet<PersonApplicationLink> PersonApplicationLinks { get; set; }
        public DbSet<PersonContactDetail> PersonContactDetails { get; set; }
        public DbSet<PersonDocument> PersonDocuments { get; set; }
        public DbSet<PersonEmail> PersonEmails { get; set; }
        public DbSet<PersonFlag> PersonFlags { get; set; }
        public DbSet<PersonFlagComment> PersonFlagComments { get; set; }
        public DbSet<PersonSms> PersonSmses { get; set; }
        public DbSet<MergePerson> MergePersons { get; set; }

        public DbSet<Tenant> Tenants { get; set; }
     
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyVoid> PropertyVoids { get; set; }
        public DbSet<PropertyUserTask> PropertyUserTasks { get; set; }
        public DbSet<PropertyDocument> PropertyDocuments { get; set; }
       

        public DbSet<Role> Roles { get; set; }
        public DbSet<Sms> Smses { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<TaskNotificationType> TaskNotifyTypes { get; set; }
        public DbSet<TaskReminderType> TaskReminderTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        #region Employee
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<UserApplicationRole> UserApplicationRoles { get; set; }
        public DbSet<UserPersonTask> UserPersonTasks { get; set; }
        public DbSet<UserDiary> UserDiaries { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserMessage> UseMessages { get; set; }
        public DbSet<UserSms> UserSmses { get; set; }
        public DbSet<UserTaskNotification> UserTaskNotifications { get; set; }
        public DbSet<UserTaskReminder> UserTaskReminders { get; set; }


        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TenantHistoryView>()
                .HasKey(o => new { o.TenantCode, o.TenancyPersonReference, o.IsMainTenant });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = 0;
            if (RecordCustomAudit)
            {
                ValidateLoggedinUser();
                var auditEntries = await OnBeforeSaveChanges();
                result = await base.SaveChangesAsync(cancellationToken);
                await OnAfterSaveChanges(auditEntries);
            }
            return result;

        }

        private void ValidateLoggedinUser()
        {
            if (string.IsNullOrWhiteSpace(CurrentLoggedUserId))
            {
                throw new ValidationException("Current logged in user needs to be set before action is performed.");
            }
            var currentLoggedUser = Users.FirstOrDefault(x => x.Subject == CurrentLoggedUserId);
            CurrentLoggedUser = currentLoggedUser ??
                                throw new ValidationException("UserId needs to be set before action is performed.");
        }

        private async Task<List<AuditEntry>> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry) { TableName = entry.Metadata.Relational().TableName };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }
                    var oldValues = (await entry.GetDatabaseValuesAsync().ConfigureAwait(false));
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = oldValues[propertyName];
                            
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                if (oldValues[propertyName] != property.CurrentValue)
                                {
                                    auditEntry.OldValues[propertyName] = oldValues[propertyName];
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Audits.Add(auditEntry.ToAudit(CurrentLoggedUser));
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private System.Threading.Tasks.Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return System.Threading.Tasks.Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit(CurrentLoggedUser));
            }
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
using System.Collections.Generic;

namespace CRM.DAL.Database.Entities.Lookup
{
    public class Lookup: BaseEntity
    {
        public Lookup()
        {
            AlertStatuses = new List<AlertStatus>();
            AlertTypes = new List<AlertType>();
            AlertGroups = new List<AlertGroup>();
            AntiSocialBehaviourCaseClosureReasons = new List<AntiSocialBehaviourCaseClosureReason>();
            AntiSocialBehaviourCaseStatuses = new List<AntiSocialBehaviourCaseStatus>();
            AntiSocialBehaviourTypes = new List<AntiSocialBehaviourType>();

            FlagTypes = new List<FlagType>();
            FlagGroups = new List<FlagGroup>();
            PersonCaseStatuses= new List<PersonCaseStatus>();
            PersonCaseTypes = new List<PersonCaseType>();
            Priorities = new List<Priority>();
            ApplicantTypes = new List<ApplicantType>();
            ContactTypes = new List<ContactType>();
            ContactByOptions = new List<ContactByOption>();
            Titles = new List<Title>();
            Genders = new List<Gender>();
            Ethnicities = new List<Ethnicity>();
            Nationalities = new List<NationalityType>();
            Languages = new List<Language>();
            Relationships = new List<Relationship>();
            DocumentTypes = new List<DocumentType>();
            ImageGroups = new List<ImageGroup>();

            EmailLabelTypes = new List<EmailLabelType>();
            EmailCategories = new List<EmailCategory>();
            EmailStatuses = new List<EmailStatus>();

            TaskTypes = new List<TaskType>();
            TaskNotificationTypes = new List<TaskNotificationType>();
            TaskReminderTypes = new List<TaskReminderType>();

        }
        public List<AlertType> AlertTypes { get; set; }
        public List<AlertStatus> AlertStatuses { get; set; }
        public List<AlertGroup> AlertGroups { get; set; }

        public List<PersonCaseStatus> PersonCaseStatuses { get; set; }
        public List<PersonCaseType> PersonCaseTypes { get; set; }
      
        public List<AntiSocialBehaviourCaseClosureReason> AntiSocialBehaviourCaseClosureReasons { get; set; }
        public List<AntiSocialBehaviourCaseStatus> AntiSocialBehaviourCaseStatuses { get; set; }
        public List<AntiSocialBehaviourType> AntiSocialBehaviourTypes { get; set; }
        public List<FlagType> FlagTypes { get; set; }
        public List<FlagGroup> FlagGroups { get; set; }
        public List<ApplicantType> ApplicantTypes { get; set; }
        public List<ContactType> ContactTypes { get; set; }
        public List<Relationship> Relationships { get; set; }

        public List<ContactByOption> ContactByOptions { get; set; }  
        public string  Name { get; set; }
        public List<Title> Titles { get; set; }
        public List<Gender> Genders { get; set; }
        public List<EmailLabelType> EmailLabelTypes { get; set; }
        public List<EmailCategory> EmailCategories { get; set; }
        public List<EmailStatus> EmailStatuses { get; set; }
        public List<Ethnicity> Ethnicities { get; set; }
        public List<NationalityType> Nationalities { get; set; }
        public List<Language> Languages { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<ImageGroup> ImageGroups { get; set; }    
        public List<TaskType> TaskTypes { get; set; }
        public List<TaskNotificationType> TaskNotificationTypes { get; set; }
        public List<TaskReminderType> TaskReminderTypes { get; set; }
        public List<Priority> Priorities { get; set; }

    }
}

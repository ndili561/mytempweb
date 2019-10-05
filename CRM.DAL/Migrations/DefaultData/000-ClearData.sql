DELETE FROM [AlertGroups]
DELETE FROM [AlertTypes]
DELETE FROM AlertStatusesDELETE FROM ImageGroups
DELETE FROM [FlagGroups]
DELETE FROM [FlagTypes]
DELETE FROM [PersonCaseTypes]
DELETE FROM [PersonCaseStatuses]
DELETE FROM [Priorities]

DELETE  FROM dbo.[EmailCategories]
DELETE  FROM [EmailStatuses] 
DELETE  FROM [EmailLabelTypes]
DELETE  FROM dbo.Tenants
DELETE  FROM dbo.PersonAddresses
DELETE  FROM dbo.PersonAntiSocialBehaviourCaseNotes
DELETE  FROM dbo.PersonAntiSocialBehaviours
DELETE  FROM dbo.PersonContactDetails
DELETE  FROM dbo.PersonApplicationLinks
DELETE  FROM Persons 
DELETE  FROM [Users]

DELETE  FROM PropertyVoids
DELETE  FROM ApplicantTypes
DELETE  FROM ContactByOptions
DELETE  FROM AddressTypes
DELETE  FROM Titles
DELETE  FROM Genders
DELETE  FROM [Languages] 
DELETE  FROM Ethnicities 
DELETE  FROM NationalityTypes 
DELETE  FROM [Permissions] 
DELETE  FROM MenuItems 
DELETE  FROM ApplicationTasks
DELETE  FROM ApplicationRoles 
DELETE  FROM Applications 
DELETE  FROM Roles
DELETE  FROM Relationships 
DELETE  FROM ContactTypes 

DELETE  FROM Addresses 
DELETE  FROM Properties 
DELETE  FROM Tenants 
DELETE  FROM dbo.AntiSocialBehaviourCaseStatuses
DELETE  FROM AntiSocialBehaviourCaseClosureReasons 
DELETE  FROM AntiSocialBehaviourTypes
DELETE  FROM DocumentTypes
DELETE  FROM TaskTypes 

DELETE  FROM Tasks 
DELETE  FROM TaskStatuses 
DELETE  FROM Lookups
--GO
DBCC CHECKIDENT ([EmailCategories], reseed, 1)
DBCC CHECKIDENT ([EmailStatuses], reseed, 1)
DBCC CHECKIDENT ([EmailLabelTypes], reseed, 1)
DBCC CHECKIDENT ([ContactTypes], reseed, 1)
DBCC CHECKIDENT ([Languages], reseed, 1)
DBCC CHECKIDENT (Genders, reseed, 1)
DBCC CHECKIDENT (Titles, reseed, 1)
DBCC CHECKIDENT (Persons, reseed, 1)
DBCC CHECKIDENT ([Users], reseed, 1)
DBCC CHECKIDENT (Ethnicities, reseed, 1)
DBCC CHECKIDENT (NationalityTypes, reseed, 1)

DBCC CHECKIDENT ([Permissions], reseed, 1)
DBCC CHECKIDENT (MenuItems, reseed, 1)
DBCC CHECKIDENT (ApplicationRoles, reseed, 1)
DBCC CHECKIDENT (Applications, reseed, 1)
DBCC CHECKIDENT (Roles, reseed, 1)
DBCC CHECKIDENT (Relationships, reseed, 1)
DBCC CHECKIDENT (Lookups, reseed, 1)
DBCC CHECKIDENT (Properties, reseed, 1)
DBCC CHECKIDENT (Tenants, reseed, 1)
--GO
IF EXISTS(SELECT * FROM sys.indexes WHERE object_id = object_id('dbo.Persons') AND NAME ='IX_Persons_ViewSearchPerson')
    DROP INDEX IX_Persons_ViewSearchPerson ON dbo.Persons;

CREATE NONCLUSTERED INDEX [IX_Persons_ViewSearchPerson] ON [dbo].[Persons]
(
	[GlobalIdentityKey] ASC
)
INCLUDE ( 	[Id],
	[DateOfBirth],
	[EthnicityId],
	[Forename],
	[GenderId],
	[NationalInsuranceNumber],
	[NationalityTypeId],
	[PreferredLanguageId],
	[Surname],
	[TitleId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--GO

--GO
SET IDENTITY_INSERT [dbo].[Persons] ON 
INSERT INTO [dbo].[Persons]
(Id,ApplicantTypeId,GlobalIdentityKey,ApplicationId,PreferredContactTime,[Forename],[Surname],[DateOfBirth],[NationalInsuranceNumber],[EthnicityId],[TitleId],[GenderId],[NationalityTypeId],[PreferredLanguageId]
)
(SELECT [ContactId]
		,ContactTypeId
		,GlobalIdentityKey
		,ApplicationId
		,PreferredContactTime
		,[Forename]
		,[Surname]
		,[DateOfBirth]
		,[NationalInsuranceNumber]
		,[EthnicityId]
		,[TitleId]
		,[GenderId]
		,[NationalityTypeId]
		,[LanguageId]
  FROM [Allocations].[dbo].[VBLContacts])
  
SET IDENTITY_INSERT [dbo].[Persons] OFF 
--GO
UPDATE p
SET
 p.[MainContactPersonId] = (SELECT Id FROM CRM.[dbo].[Persons] 
							WHERE GlobalIdentityKey = (SELECT GlobalIdentityKey 
														FROM Allocations.dbo.VBLContacts 
														WHERE ApplicationId = a.ApplicationId 
														AND ContactTypeId = 1))
FROM CRM.dbo.Persons p 
INNER JOIN Allocations.dbo.VBLContacts c
ON p.GlobalIdentityKey = c.GlobalIdentityKey
INNER JOIN Allocations.dbo.VBLApplications a
ON c.ApplicationId = a.ApplicationId
--

UPDATE p
SET
 p.[NationalInsuranceNumber] = t.NHINO

FROM CRM.dbo.Persons p 
INNER JOIN [MasterReferenceData].[dbo].[TBL_ASB_LINK] t
ON p.Forename = t.Forenames
AND p.Surname = t.Surname
AND p.[DateOfBirth] = t.DOB
WHERE p.[NationalInsuranceNumber] IS NULL
AND t.NHINO != ''

--GO

INSERT INTO [dbo].[Tenants]
([PersonId],[PropertyId],[TenancyReference],[TenancyType],[TenantCode])
(SELECT 1,1,TENANCYREF,TENANCYTYPE,TENANT_CODE FROM [MasterReferenceData].[dbo].[TBL_CUSTOMER])


UPDATE t
SET t.PropertyId = p.Id
FROM CRM.dbo.Tenants t
INNER JOIN [MasterReferenceData].[dbo].[TBL_CUSTOMER] c
ON t.TenantCode = c.TENANT_CODE
AND t.TenancyReference = c.TENANCYREF
INNER JOIN CRM.dbo.Properties p
ON c.PROPERTYREF = p.PropertyCode

UPDATE t
SET t.PersonId = p.Id
FROM CRM.dbo.Tenants t
INNER JOIN [MasterReferenceData].[dbo].[TBL_CUSTOMER] c
ON t.TenantCode = c.TENANT_CODE
AND t.TenancyReference = c.TENANCYREF
INNER JOIN CRM.dbo.Persons p
ON c.FIRSTNAME = p.Forename
AND c.LASTNAME = p.Surname
AND c.DOB = p.DateOfBirth

UPDATE p
SET p.TenantCode = c.TENANT_CODE,
p.TenantId = t.Id
FROM CRM.dbo.Tenants t
INNER JOIN [MasterReferenceData].[dbo].[TBL_CUSTOMER] c
ON t.TenantCode = c.TENANT_CODE
AND t.TenancyReference = c.TENANCYREF
INNER JOIN CRM.dbo.Persons p
ON c.FIRSTNAME = p.Forename
AND c.LASTNAME = p.Surname
AND c.DOB = p.DateOfBirth


INSERT INTO [dbo].[Persons]
(GlobalIdentityKey,TenantId,[Forename],[Surname],[DateOfBirth],TenantCode,[NationalInsuranceNumber],EthnicityId,TitleId,GenderId)
(
SELECT NEWID(),t.Id,ch.TENANT_FIRST_NAME,ch.TENANT_LAST_NAME,ch.TENANT_DOB,ch.[PERSON-REF],NI_NUMBER,
(SELECT Id FROM Ethnicities WHERE Name LIKE RTRIM(LTRIM(ch.TENANT_ETHNICITY) ) +'%') EthnicityId,
(SELECT Id FROM Titles WHERE Name = RTRIM(LTRIM(ch.TENANT_TITLE) )) TitleId,
(SELECT Id FROM Genders WHERE Name LIKE + RTRIM(LTRIM(ch.TENANT_GENDER) ) +'%' AND IsActive =1) GenderId
FROM [MasterReferenceData].[dbo].[TBL_CUSTOMER_HISTORIC] ch
INNER JOIN CRM.dbo.Tenants t
ON t.TenantCode = ch.[PERSON-REF]
WHERE t.TenantCode NOT IN (SELECT TenantCode FROM CRM.dbo.Tenants WHERE PersonId  >1)
)

UPDATE t
SET t.PersonId = p.Id
FROM CRM.dbo.Tenants t
INNER JOIN CRM.dbo.Persons p
ON t.TenantCode = p.TenantCode
AND t.PersonId  = 1


INSERT INTO [dbo].[Persons]
(GlobalIdentityKey,[Forename],[Surname],[DateOfBirth],TenantCode,[NationalInsuranceNumber],EthnicityId,TitleId,GenderId)
(
SELECT NEWID(),ch.TENANT_FIRST_NAME,ch.TENANT_LAST_NAME,ch.TENANT_DOB,ch.[PERSON-REF],NI_NUMBER,
(SELECT Id FROM Ethnicities WHERE Name LIKE RTRIM(LTRIM(ch.TENANT_ETHNICITY) ) +'%') EthnicityId,
(SELECT Id FROM Titles WHERE Name = RTRIM(LTRIM(ch.TENANT_TITLE) )) TitleId,
(SELECT Id FROM Genders WHERE Name LIKE + RTRIM(LTRIM(ch.TENANT_GENDER) ) +'%' AND IsActive =1) GenderId
FROM [MasterReferenceData].[dbo].[TBL_CUSTOMER_HISTORIC] ch
WHERE ch.[PERSON-REF] NOT IN (SELECT TenantCode FROM CRM.dbo.Tenants WHERE PersonId  >1)
AND (ISNULL(ch.NI_NUMBER,'')='' OR  ch.NI_NUMBER NOT IN (SELECT NationalInsuranceNumber FROM CRM.dbo.Persons))
)
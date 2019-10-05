--GO

INSERT INTO [dbo].[PersonAntiSocialBehaviours]
(IBSCaseReference,[LoggedDate],[CompletionDate],[UserId])
(SELECT DISTINCT CommRef,[LoggedDate],[CompletionDate],(SELECT TOP 1 Id FROM Users) FROM [MasterReferenceData].[dbo].[TBL_ASB_SUMMARY] asb)


UPDATE  asb
SET asb.PersonId = p.Id
FROM [PersonAntiSocialBehaviours] asb 
INNER JOIN [MasterReferenceData].[dbo].[TBL_ASB_SUMMARY] mra
ON asb.IBSCaseReference = mra.CommRef
INNER JOIN [MasterReferenceData].[dbo].[TBL_ASB_LINK] asl
ON mra.CommRef = asl.CommRef
INNER JOIN CRM.[dbo].[Persons] p 
ON asl.[NHINO] = p.NationalInsuranceNumber
AND p.NationalInsuranceNumber IS NOT NULL




--GO
SET IDENTITY_INSERT [dbo].[PersonAntiSocialBehaviourCaseNotes] ON 
INSERT INTO [dbo].[PersonAntiSocialBehaviourCaseNotes]
(Id,PersonAntiSocialBehaviourId,Note)
(SELECT asn.Id
		,pasb.Id
		,asn.[DiaryNote]
		
  FROM [MasterReferenceData].[dbo].[TBL_ASB_NOTES] asn
  INNER JOIN [crm].[dbo].[PersonAntiSocialBehaviours] pasb
  ON asn.CommRef = pasb.IBSCaseReference
 )
 SET IDENTITY_INSERT [dbo].[PersonAntiSocialBehaviourCaseNotes] OFF 

 
 UPDATE pasn
 SET  pasn.ActionDateTime=CASE ISDATE((asn.[EntryDate] + CAST(REPLACE(REPLACE( [EntryTime],'.',':'),'::',':') AS DATETIME))) WHEN 1 THEN (asn.[EntryDate] + CAST( REPLACE(REPLACE( [EntryTime],'.',':'),'::',':') AS DATETIME)) ELSE CASE ISDATE(asn.[EntryDate]) WHEN 1 THEN asn.[EntryDate] ELSE NULL END END
 FROM MasterReferenceData.[dbo].[TBL_ASB_NOTES]  asn
 INNER JOIN [PersonAntiSocialBehaviourCaseNotes] pasn
 ON asn.Id = pasn.Id
 WHERE ISDATE(REPLACE(REPLACE( [EntryTime],'.',':'),'::',':')) = 1

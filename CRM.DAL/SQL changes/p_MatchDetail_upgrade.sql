USE [CloudVoids]
GO
/****** Object:  StoredProcedure [dbo].[p_MatchDetail]    Script Date: 26/04/2018 13:26:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================      
-- Author:  Sean Maller      
-- Create date: 20-10-15      
-- Description: Returns match data      
-- 08/12/2015 Paul Firth: Added Telephone and Mobile info (email from Jason Weir 08/12/2015)     
-- 26/04/2018 Nico DiLillo: Corrected SP to look at new CRM database. 
-- =============================================      
ALTER PROCEDURE [dbo].[p_MatchDetail] @TaskId AS INT = NULL
AS
BEGIN
       SET NOCOUNT ON

       SELECT TOP 1 T.DateCreated
              ,ANU.UserName AS Username
              ,CONCAT (
                     ANU.FirstName
                     ,' '
                     ,ANU.LastName
                     ) AS CreatedBy
              ,C.ApplicationId AS CustomerApplicationId
              ,CI.CustomerInterestStatusID
              ,CIS.[Name] AS MatchStatus
              ,T.TaskStatusId
              ,TS.TaskStatusName AS TaskStatus
              ,PM.TaskOutcome
              ,T.TaskOutcomeNotes
              ,SC.Title
              ,SC.Forename
              ,SC.Surname
              ,SC.DateOfBirth
              ,vblAddress.AddressLine1 AS Address1
              ,vblAddress.AddressLine2 AS Address2
              ,vblAddress.PostCode AS PostCode
              ,CD.Telephone AS Telephone
              ,CD.Mobile AS Mobile
              
              ,CASE
              WHEN
              (SELECT LookupCode FROM  CloudVoids.dbo.[Lookup] WHERE LookupCode = LTRIM(RTRIM(CI.OutcomeNotes))) IS NOT NULL
              THEN
              (SELECT LookupDescription FROM  CloudVoids.dbo.[Lookup] WHERE LookupCode = CI.OutcomeNotes)
              ELSE
              CI.OutcomeNotes
        END
              AS OutcomeNotes

              
              ,CASE 
                     WHEN CI.CustomerDecision = 1
                           THEN 'Yes'
                     WHEN CI.CustomerDecision = 0
                           THEN 'No'
                     ELSE 'Unknown'
                     END AS CustomerDecision
              ,RR.LookupDescription AS Reason
              ,ISNULL(PM.IsDirectLet, 0) AS IsDirectLet
              ,DLR.[Name] AS DirectLetReason
       FROM dbo.Task AS T
       JOIN dbo.Void AS V ON V.VoidId = T.VoidId
       JOIN dbo.PropertyMatch AS PM ON T.TaskId = PM.TaskId
       LEFT JOIN [Identity].dbo.AspNetUsers AS ANU ON ANU.Id = T.CreatedById
       LEFT JOIN dbo.Lookup AS RR ON PM.RejectionReason = RR.LookupCode
              AND RR.LookupGroup = 'RejectionReason'
       LEFT JOIN Allocations.dbo.VBLContacts AS C ON C.ApplicationId = PM.CustomerApplicationId
              AND C.ContactTypeID = 1
	   LEFT JOIN Allocations.dbo.SearchContacts AS SC ON SC.GlobalIdentityKey = C.GlobalIdentityKey
       LEFT JOIN Allocations.dbo.VBLCustomerInterests AS CI ON CI.ApplicationId = C.ApplicationId
              AND CI.PropertyCode = V.PropertyCode
              AND CI.CustomerInterestStatusId != 11
       LEFT JOIN Allocations.dbo.CustomerInterestStatus AS CIS ON CI.CustomerInterestStatusID = CIS.CustomerInterestStatusID
       LEFT JOIN dbo.TaskStatus AS TS ON TS.TaskStatusId = T.TaskStatusId
       LEFT JOIN [Allocations].[dbo].[VBLAddresses] AS vblAddress ON C.ApplicationId = vblAddress.ApplicationId
       LEFT JOIN dbo.DirectLetReason AS DLR ON DLR.Id = PM.DirectLetReasonId
       OUTER APPLY (
              SELECT [Mobile]
                     ,[Telephone]
              FROM (
                     SELECT CASE CD.ContactById
                                  WHEN 1
                                         THEN 'Telephone'
                                  WHEN 3
                                         THEN 'Mobile'
                                  ELSE 'NoData'
                                  END AS ContactType
                           ,CD.ContactValue
                     FROM Allocations.dbo.VBLContactbyDetails AS CD
                     WHERE CD.ContactById IN (
                                  1
                                  ,3
                                  )
                           AND CD.contactid = C.ContactId
                     ) AS SourceTable
              PIVOT(MAX(ContactValue) FOR ContactType IN (
                                  [Mobile]
                                  ,[Telephone]
                                  )) AS PivotTable
              ) AS CD
       WHERE T.TaskId = @TaskId
       ORDER BY CI.CustomerInterestStatusId DESC;

       SET NOCOUNT OFF
END;


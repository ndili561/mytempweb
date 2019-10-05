--GO
INSERT INTO [dbo].[PersonContactDetails]
(PersonId,[ContactByOptionId],[ContactValue],[Comment],[IsDefault])
(SELECT p.Id,cd.[ContactById],cd.[ContactValue],cd.[ContactText],1
  FROM [dbo].[Persons] p  
  INNER JOIN [Allocations].[dbo].[VBLContacts] c
  ON p.GlobalIdentityKey = c.GlobalIdentityKey
  INNER JOIN [Allocations].[dbo].[VBLContactByDetails] cd
  ON c.ContactId = cd.ContactId
  )
--GO

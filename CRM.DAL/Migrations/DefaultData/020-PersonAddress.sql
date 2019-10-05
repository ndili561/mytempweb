--GO
INSERT INTO [dbo].[PersonAddresses]
(PersonId,AddressId,AddressTypeId)
(SELECT p.Id,a.AddressId,1
  FROM [dbo].[Persons] p  
  INNER JOIN [Allocations].[dbo].[VBLContacts] c
  ON p.GlobalIdentityKey = c.GlobalIdentityKey
  INNER JOIN [Allocations].[dbo].[VBLAddresses] a
  ON c.ApplicationId = a.ApplicationId
  )
--GO

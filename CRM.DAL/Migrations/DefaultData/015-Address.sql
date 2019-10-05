--GO
 SET IDENTITY_INSERT [dbo].[Addresses] ON 
INSERT INTO [dbo].[Addresses]
(Id,
AddressLine1,
AddressLine2,
AddressLine3,
AddressLine4,
PostCode,
IsActive)
(
SELECT 
AddressId,
AddressLine1,
AddressLine2,
AddressLine3,
AddressLine4,
PostCode,
IsActive
  FROM [Allocations].[dbo].[VBLAddresses])

 SET IDENTITY_INSERT [dbo].[Addresses] OFF
 --GO

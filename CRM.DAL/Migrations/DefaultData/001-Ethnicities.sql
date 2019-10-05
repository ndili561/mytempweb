
SET IDENTITY_INSERT [dbo].[Lookups] ON 
INSERT [dbo].[Lookups] 
([Id], [Name]) 
VALUES 
(1, N'CRM Lookups')

SET IDENTITY_INSERT [dbo].[Lookups] OFF

SET IDENTITY_INSERT [dbo].[Ethnicities] ON 
INSERT [dbo].[Ethnicities] 
([Id], [Name],[LookupId], [IsActive], [SortOrder], [IBSCode]) 
VALUES 
(1, N'Asian or Asian British Bangladeshi',1,1, 1, 10)
,(2, N'Asian or Asian British Indian',1,1, 2, 8)
,(3, N'Asian or Asian British Other',1,1, 3, 11)
,(4, N'Asian or Asian British Pakistani',1,1, 4, 9)
,(5, N'Asian or Asian British Chinese',1,1, 5, 15)
,(6, N'Black or Black British African',1,1, 6, 13)
,(7, N'Black or Black British Caribbean',1,1, 7, 12)
,(8, N'Black or Black British Other',1,1, 8, 14)
,(9, N'Mixed Other',1,1, 9, 7)
,(10, N'Mixed White and Asian',1,1, 10, 6)
,(11, N'Mixed White and Black African',1,1, 11, 5)
,(12, N'Mixed White and Black Caribbean',1,1, 12, 4)
,(13, N'Other Ethnic Group - Arab',1,1, 14, 19)
,(14, N'Other Ethnic Group - Other',1,1, 15, 16)
,(15, N'Refused',1,1, 16, 17)
,(16, N'White British',1,1, 17, 1)
,(17, N'White Gypsy, Irish Traveller',1,1, 18, 18)
,(18, N'White Irish',1,1, 19, 2)
,(19, N'White Other',1,1, 20, 3)
,(20, N'MigrationError',1, 0, 999, 0)
SET IDENTITY_INSERT [dbo].[Ethnicities] OFF
--GO

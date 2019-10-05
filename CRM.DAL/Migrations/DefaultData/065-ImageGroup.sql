--GO
SET IDENTITY_INSERT [dbo].[ImageGroups] ON 
INSERT [dbo].[ImageGroups] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'Property Shop',1,1, 1)
,(2, N'Health and Safety',1,1, 2)
,(3, N'Property Inspection',1,1, 3)
,(4, N'Property Survey',1,1, 4)

SET IDENTITY_INSERT [dbo].[ImageGroups] OFF

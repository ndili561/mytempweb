--GO
SET IDENTITY_INSERT [dbo].[AddressTypes] ON 
INSERT [dbo].[AddressTypes] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
 (1, N'Main',1,1, 1)
,(2, N'Contact',1,1, 2)
,(3, N'Temprory',1, 0, 3)
SET IDENTITY_INSERT [dbo].[AddressTypes] OFF
--GO

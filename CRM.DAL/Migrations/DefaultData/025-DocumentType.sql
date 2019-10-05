--GO
SET IDENTITY_INSERT [dbo].[DocumentTypes] ON 
INSERT [dbo].[DocumentTypes] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
 (1, N'Image',1,1, 10)
,(2, N'Excel',1,1, 20)
,(3, N'Word',1,1, 30)
,(4, N'PDF',1,1, 40)
,(5, N'CSV',1,1, 50)
,(6, N'Email',1,1, 60)
SET IDENTITY_INSERT [dbo].[DocumentTypes] OFF
--GO

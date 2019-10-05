

SET IDENTITY_INSERT [dbo].[Genders] ON 
INSERT [dbo].[Genders] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'Female',1,1, 1)
,(2, N'Male',1,1, 2)
,(3, N'MigrationError',1, 0, 999)
SET IDENTITY_INSERT [dbo].[Genders] OFF
--GO


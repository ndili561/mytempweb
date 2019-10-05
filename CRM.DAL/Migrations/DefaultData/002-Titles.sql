SET IDENTITY_INSERT [dbo].[Titles] ON 
INSERT [dbo].[Titles] ([Id], [Name],[LookupId], [IsActive], [SortOrder], [DefaultGenderID]) 
VALUES 
(0, N'Unknown',1, 0, 0, null)
,(1, N'Cllr', 1,0, 9, null)
,(2, N'Dr',1, 0, 6, null)
,(3, N'Lord',1, 0, 100, 2)
,(4, N'Miss',1,1, 50, 1)
,(5, N'Mr',1,1, 10, 2)
,(6, N'Mrs',1,1, 20, 1)
,(7, N'Rev',1, 0, 7,null)
,(8, N'Sir',1, 0, 8, 2)
,(9, N'Mstr',1, 0, 40, 2)
,(10, N'Ms',1,1, 30, 1)
,(11, N'Master',1,1, 50, 2)
,(12, N'MigrationError',1, 0, 999, null)
SET IDENTITY_INSERT [dbo].[Titles] OFF
--GO

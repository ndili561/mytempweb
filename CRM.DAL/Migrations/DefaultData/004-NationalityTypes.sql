SET IDENTITY_INSERT [dbo].[NationalityTypes] ON 
INSERT [dbo].[NationalityTypes] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'Bulgaria',1,1, 1)
,(2, N'Czech Republic',1,1, 2)
,(3, N'Estonia',1,1, 3)
,(4, N'Hungary',1,1, 4)
,(5, N'Latvia',1,1, 5)
,(6, N'Lithuania',1,1, 6)
,(7, N'Non EEA',1,1, 7)
,(8, N'Other EEA',1,1, 8)
,(14, N'MigrationError',1, 0, 999)
,(10, N'Romania',1,1, 10)
,(11, N'Slovenia',1,1, 11)
,(12, N'Slovakia',1,1, 12)
,(13, N'UK National',1,1, 13)
,(9, N'Polish',1,1, 9)
SET IDENTITY_INSERT [dbo].[NationalityTypes] OFF
--GO

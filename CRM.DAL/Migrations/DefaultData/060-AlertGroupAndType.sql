--GO
SET IDENTITY_INSERT [dbo].[AlertGroups] ON 
INSERT [dbo].[AlertGroups] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'System',1,1, 1)
,(2, N'Manual',1,1, 2)

SET IDENTITY_INSERT [dbo].[AlertGroups] OFF
--GO
SET IDENTITY_INSERT [dbo].[AlertTypes] ON 
INSERT [dbo].[AlertTypes] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'DebtAssist',1,1, 1)
,(2, N'GasServicing',1,1, 2)
,(3, N'Key Due',1,1, 3)
,(4, N'Document Required',1,1, 4)
,(5, N'VBL Match',1,1, 5)
SET IDENTITY_INSERT [dbo].[AlertTypes] OFF

SET IDENTITY_INSERT [dbo].[FlagGroups] ON 
INSERT [dbo].[FlagGroups] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'General',1,1, 1)
,(2, N'Asbestos',1,1, 2)
,(3, N'AntiSocialBehaviour',1,1, 3)
,(4, N'VBL Application Status',1,1, 4)
,(5, N'Universal Credit',1,1, 5)
,(6, N'Repair',1,1, 6)
,(7, N'PropertyRedFlags',1,1, 7)
SET IDENTITY_INSERT [dbo].[FlagGroups] OFF
--GO
SET IDENTITY_INSERT [dbo].[FlagTypes] ON 
INSERT [dbo].[FlagTypes] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'ActionRequired',1,1, 1)
,(2, N'Information',1,1, 2)
,(3, N'Query',1,1, 3)
,(4, N'General',1,1, 3)
SET IDENTITY_INSERT [dbo].[FlagTypes] OFF

SET IDENTITY_INSERT [dbo].[PersonCaseTypes] ON 
INSERT [dbo].[PersonCaseTypes] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'General',1,1, 1)
,(2, N'VBL Application',1,1, 2)
,(3, N'Voids',1,1, 3)
,(4, N'Appointment',1,1, 4)
,(5, N'Query',1,1, 5)
,(6, N'Reapir',1,1, 6)
,(7, N'Complain',1,1, 7)
SET IDENTITY_INSERT [dbo].[PersonCaseTypes] OFF
SET IDENTITY_INSERT [dbo].[PersonCaseStatuses] ON 
INSERT [dbo].[PersonCaseStatuses] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'Open',1,1, 1)
,(2, N'InProgress',1,1, 2)
,(3, N'Cancelled',1,1, 3)
,(4, N'Closed',1,1, 4)
,(5, N'Suspended',1,1, 5)
,(6, N'ReOpen',1,1, 6)

SET IDENTITY_INSERT [dbo].[PersonCaseStatuses] OFF

SET IDENTITY_INSERT [dbo].[Priorities] ON 
INSERT [dbo].[Priorities] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'Urgent',1,1, 1)
,(2, N'High',1,1, 2)
,(3, N'Medium',1,1, 3)
,(4, N'Low',1,1, 4)
SET IDENTITY_INSERT [dbo].[Priorities] OFF

SET IDENTITY_INSERT [dbo].[AlertStatuses] ON 
INSERT [dbo].[AlertStatuses] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'Active',1,1, 1)
,(2, N'On Hold',1,1, 2)
,(3, N'Closed',1,1, 3)
,(4, N'Cancelled',1,1, 4)
SET IDENTITY_INSERT [dbo].[AlertStatuses] OFF


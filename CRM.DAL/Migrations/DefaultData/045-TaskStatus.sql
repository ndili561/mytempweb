--GO
SET IDENTITY_INSERT [dbo].[TaskStatuses] ON 
INSERT [dbo].[TaskStatuses] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'Created',1,1, 1)
,(2, N'Assigned',1,1, 2)
,(3, N'Inprogress',1,1, 3)
,(4, N'Completed',1,1, 4)
,(5, N'Closed',1,1, 5)
,(6, N'Cancelled',1,1, 6)
,(7, N'Imported',1,1, 6)
SET IDENTITY_INSERT [dbo].[TaskStatuses] OFF
--GO
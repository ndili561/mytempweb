--GO

SET IDENTITY_INSERT [dbo].[EmailCategories] ON 
INSERT [dbo].[EmailCategories] 
([Id],[CssClass], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
 (1, N'warning', N'Work',1,1, 10)
,(2, N'danger', N'Documents',1,1, 20)
,(3, N'primary', N'Social',1,1, 30)
,(4, N'navy', N'Advertising',1,1, 40)
,(5, N'success', N'Clients',1,1, 50)
SET IDENTITY_INSERT [dbo].[EmailCategories] OFF


SET IDENTITY_INSERT [dbo].[EmailStatuses] ON 
INSERT [dbo].[EmailStatuses] 
([Id],[CssClass],  [Name],[LookupId],  [IsActive], [SortOrder]) 
VALUES 
 (1, N'info', N'Draft',1,1, 10)
,(2, N'warning', N'Send',1,1, 20)
,(3, N'success', N'Delivered',1,1, 30)
,(4, N'danger', N'Failed',1,1, 40)
,(5, N'navy', N'Read',1,1, 50)
SET IDENTITY_INSERT [dbo].[EmailStatuses] OFF

SET IDENTITY_INSERT [dbo].[EmailLabelTypes] ON 
INSERT [dbo].[EmailLabelTypes] 
([Id], [CssClass], [Name],[LookupId],  [IsActive], [SortOrder]) 
VALUES 
 (1,N'success', N'Repair',1,1, 10)
,(2,N'danger', N'Voids',1,1, 20)
,(3,N'pink', N'Enquiry',1,1, 30)
,(4,N'navy', N'Complain',1,1, 40)
,(5,N'warning', N'Rent',1,1, 50)
,(6,N'primary', N'Domistics Violance',1,1, 60)
,(7,N'info', N'Police Enquiry',1,1, 70)
SET IDENTITY_INSERT [dbo].[EmailLabelTypes] OFF
--GO

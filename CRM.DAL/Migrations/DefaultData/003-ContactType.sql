SET IDENTITY_INSERT [dbo].[ContactTypes] ON 
--GO
INSERT [dbo].[ContactTypes] ([Id], [Name],  [SortOrder],[IsActive],[LookupId]) 
VALUES 
(1, N'ThirdParty',1,1,1)
,(2, N'Forwarding',2,1,1)
,(3, N'Correspondence',3,1,1)
,(4, N'NextOfKin',4,1,1)
,(5, N'ContactPerson',5,1,1)
																		
SET IDENTITY_INSERT [dbo].[ContactTypes] OFF
--GO

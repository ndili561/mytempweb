SET IDENTITY_INSERT [dbo].[ApplicantTypes] ON 
--GO
INSERT [dbo].[ApplicantTypes] ([Id], [Name],  [SortOrder],[IsActive],[LookupId]) 
VALUES 
(0, N'Unkown',1,1,1)
,(1, N'Applicant',2,1,1)
,(2, N'Joint Applicant',3,1,1)
,(3, N'Household Member',4,1,1)
																		
SET IDENTITY_INSERT [dbo].[ApplicantTypes] OFF
--GO

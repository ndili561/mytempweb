--GO
SET IDENTITY_INSERT [dbo].[ContactByOptions] ON 
INSERT [dbo].[ContactByOptions] 
([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1, N'Phone',1,1, 1)
,(2, N'3rd Party',1,1, 6)
,(3, N'Mobile',1,1, 3)
,(4, N'Email',1,1, 4)
,(5, N'Mail',1,1, 5)
,(6, N'Minicom',1,1, 2)
,(7, N'Braille',1,1, 7)
,(8, N'Mobile Text',1,1, 8)
SET IDENTITY_INSERT [dbo].[ContactByOptions] OFF
 --GO

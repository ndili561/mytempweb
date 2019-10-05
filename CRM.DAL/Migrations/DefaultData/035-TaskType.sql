--GO
SET IDENTITY_INSERT [dbo].[TaskTypes] ON 
INSERT [dbo].[TaskTypes] 
([Id],[CssStyle],[CssClass],[IsActive],[LookupId],[SortOrder],[Name]) 
VALUES
(1,'background-color:#1a7bb9;','warning',1,1,1,'outlook'),
(2,'background-color:#1a7bb9;','purple',1,1,2,'Meeting'),
(3,'background-color:#1D8EBA;','danger',1,1,3,'Holiday'),
(4,'background-color:#F7DA9E;','navy',1,1,4,'OnLeave'),
(5,'background-color:#1ab394;','primary',1,1,5,'Miscellaneous'),
(6,'background-color:#A0321D;','success',1,1,6,'Applications')
SET IDENTITY_INSERT [dbo].[TaskTypes] OFF
--GO
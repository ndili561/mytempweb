--GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
INSERT [dbo].[Roles] ([Id], [RoleName], [RoleDescription], [IsAdministrator], [IsActive]) 
VALUES 
(1, N'Logistics Administrator', N'Full Access', 1, 1)
,(2, N'Logistics Procurement Manager', N'Stock reorder,Approve Purchase Order', 0,  1)
,(3, N'Logistics Warehouse Manager', N'Create,Assign and Approve stock take task,upload price book, add new product, product type, group, subgroup, supplier, contract,Approve Purchase Order', 0, 1)
,(4, N'Logistics Warehouse Supervisor', N'Perform stock take task,add warehouse location,stock transfer,stock return', 0, 1)
,(5, N'Logistics Dispatch Officer', N'Add edit remove and track delivery van,driver and dispatch the requisition', 0, 1)
,(6, N'Logistics Picking', N'Pick the items from warehouse and place it on disptach location', 0, 1)
,(7, N'Logistics --GOods Receipt', N'Receive the proof of delivery, update GRN ,scan and upload POD file and place the --GOods in --GOOSIN Area.', 0, 1)
,(8, N'Logistics Officer', N'Create requisition for all types of requirements (i.e.,Repairs,Voids,Warehouse Issue etc.).', 0, 1)
,(9, N'Logistics Accounts Payable Clerk', N'Register Supplier Invoice and run Open Account Payment Interface.', 0, 1)
,(10, N'Logistics Accounts Payable Manager', N'View Invoice, Purchase Order and can update Tolerance limit.', 0, 1)
,(11, N'Logistics Operative Manager', N'Add operative, stocks, operative manager, skills ,approve operative stock take task.', 0, 1)
,(12, N'Logistics Operative', N'Perform stock take task manually, can create requisition in future only for the current repair task assiged to him.', 0, 1)
,(13, N'Logistics User Management', N'Add remove permission to Roles.', 0, 1)
,(100, N'Voids Administrator', N'Full Access', 1, 1)
,(200, N'VBL Administrator', N'Full Access', 1, 1)
,(300, N'CRM Administrator', N'Full Access', 1, 1)

SET IDENTITY_INSERT [dbo].[Roles] OFF
--GO

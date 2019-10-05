
--GO
SET IDENTITY_INSERT [dbo].[MenuItems] ON 

INSERT [dbo].[MenuItems] ([Id],[ApplicationId], [ControllerName], [DisplayName]) 
VALUES 
 (0,1,N'Audit',N'Audit')
,(1,1,N'CreditNote',N'CreditNote')
,(2,1,N'DispatchNote',N'DispatchNote')
,(3,1,N'Delivery',N'Delivery')
,(4,1,N'GoodsIn',N'GoodsIn')
,(5,1,N'Invoice',N'Invoice')
,(6,1,N'LogisticsSetting',N'LogisticsSetting')
,(7,1,N'Manufacturer',N'Manufacturer')
,(8,1,N'Operative',N'Operative')
,(9,1,N'OperativeManager',N'OperativeManager')
,(10,1, N'InterfaceCreditNoteToOpenAccount', N'InterfaceCreditNoteToOpenAccount')
,(11,1, N'InterfaceInvoiceToOpenAccount', N'InterfaceInvoiceToOpenAccount')
,(12,1, N'PermanentJob', N'PermanentJob')
,(13,1, N'PickNote', N'PickNote')
,(14,1, N'Product', N'Product')
,(15,1, N'ProductGroup', N'ProductGroup')
,(16,1, N'ProductSubGroup', N'ProductSubGroup')
,(17,1, N'ProductSupplier', N'ProductSupplier')
,(18,1, N'ProductType', N'ProductType')
,(19,1, N'PurchaseOrder', N'PurchaseOrder')
,(20,1, N'ResolvePayment', N'ResolvePayment')
,(21,1, N'ResolveCreditNote', N'ResolveCreditNote')
,(22,1, N'PriceChange', N'PriceChange')
,(23,1, N'ExternalRequisition', N'ExternalRequisition')
,(24,1, N'RepairRequisition', N'RepairRequisition')
,(25,1, N'WarehouseIssueRequisition', N'WarehouseIssueRequisition')
,(26,1, N'OperativeStockRequisition', N'OperativeStockRequisition')
,(27,1, N'VoidsRequisition', N'VoidsRequisition')
,(28,1, N'VanStockRequisition', N'VanStockRequisition')
,(29,1, N'Stock', N'Stock')
,(30,1, N'StockOrder', N'StockOrder')
,(31,1, N'StockReturns', N'StockReturns')
,(32,1, N'Stocktake', N'Stocktake')
,(33,1, N'StocktakeApprove', N'StocktakeApprove')
,(34,1, N'StocktakeComplete', N'StocktakeComplete')
,(35,1, N'StocktakeCreate', N'StocktakeCreate')
,(36,1, N'StockTransaction', N'StockTransaction')
,(37,1, N'StockTransfer', N'StockTransfer')
,(38,1, N'Supplier', N'Supplier')
,(39,1, N'SupplierContract', N'SupplierContract')
,(40,1, N'SupplierPriceFile', N'SupplierPriceFile')
,(41,1, N'SupplierProduct', N'SupplierProduct')
,(42,1, N'SupplierType', N'SupplierType')
,(43,1, N'Tracking', N'Tracking')
,(44,1, N'UnitOfIssue', N'UnitOfIssue')
,(45,1, N'Van', N'Van')
,(46,1, N'Warehouse', N'Warehouse')
,(47,1, N'WarehouseLayout', N'WarehouseLayout')
,(48,1, N'WarehouseProduct', N'WarehouseProduct')
,(49,1, N'WarehouseStock', N'WarehouseStock')

,(101,6, N'Person', N'Person')
,(102,6, N'Audit', N'Audit')
,(103,6, N'User', N'Employee')
,(104,6, N'Email', N'Person')
,(105,6, N'Document', N'Document')
,(106,6, N'Calendar', N'Calendar')
,(107,6, N'Lookup', N'Lookup')
--GO




























SET IDENTITY_INSERT [dbo].[MenuItems] OFF
--GO

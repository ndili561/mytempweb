--GO

INSERT INTO [dbo].[Properties]
(PropertyCode)
(SELECT PropertyCode FROM [Assets].[dbo].[Asset])

--GO

INSERT INTO [dbo].[PropertyVoids]
(PropertyId,VoidId)
(SELECT p.Id,v.VoidId FROM [CloudVoids].[dbo].[Void] v INNER JOIN CRM.dbo.Properties p ON   p.PropertyCode = v.PropertyCode WHERE v.VoidId != -1 )

--GO
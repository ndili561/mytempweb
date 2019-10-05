--GO
INSERT INTO [dbo].[PersonApplicationLinks]
([ApplicationId],[IsActive],[PersonId])
(SELECT 5,1,Id  FROM [dbo].[Persons])
--GO

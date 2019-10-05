--GO
  INSERT [dbo].[Users] ([Name],Email,PersonId,[subject],[IsSysAdmin],IsActive) 
(SELECT ISNULL(FirstName +' ' +LastName,''),Email,PersonId,Id,0,1 FROM [Identity].dbo.[AspNetUsers])
--GO

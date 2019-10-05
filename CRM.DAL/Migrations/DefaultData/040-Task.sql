--GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 
INSERT [dbo].[Tasks] 
([Id], [Name],[Comment],TaskDuration,IsActive) 
 
 (SELECT  [TaskTypeId],[TaskTypeName],[TaskTypeDescription],0,1
     
  FROM [CloudVoids].[dbo].[TaskType])
SET IDENTITY_INSERT [dbo].[Tasks] OFF


INSERT INTO [dbo].[ApplicationTasks]
([ApplicationId],[Comment],[TaskId])
(SELECT  (Select Id FROM Applications WHERE Name ='Voids'),'',Id  FROM [Tasks])
--GO
namespace CRM.DAL.Migrations.SQLScript
{
   public partial class SqlProgrammability    {

       public static string Drop_PropertyVoidView
        {
           get { return @"IF  EXISTS (SELECT * FROM sys.views
                                  WHERE object_id = OBJECT_ID(N'dbo.PropertyVoidView'))
                                  DROP VIEW dbo.PropertyVoidView"; } 
       }


        public static string Create_PropertyVoidView
        {
            get { return @"EXEC ('Create View [dbo].[PropertyVoidView] AS 
                        SELECT 
		[VoidId]
      ,[PropertyCode]
      ,[VoidStatusId]
      ,[IBSTenancyEndDate]
      ,[TenancyStatus]
      ,[TenancyType]
      ,[TenancyRef]
      ,[SiteManagerUserId]
      ,[NHOUserId]
      ,[IsReadyForMatching]
      ,[SignedTerminationReason]
      ,[NewTenancyRef]
      ,[HasCustomerSignedTenancyAgreement]
      ,[CustomerSignUpDate]
      ,[CustomerSignUpIBSTenancyStartDate]
      ,[CustomerSignUpTenancyType]
      ,[IsReadyForPropertyShop]
      ,[HoldReleaseDate]
      ,[IBSTenancyStartDate]
      ,[StarterTenancyID]
      ,[SendToPropertyShop]
      ,[LastModifiedById]
      ,[TenancyEndTaskId]
      ,[PropertyInspectionTaskId]
      ,[PropertyMatchTaskId]
      ,[PropertyHandoverTaskId]
      ,[TerminationTaskId]
      ,[CustomerViewingTaskId]
      ,[CustomerSignupTaskId]
      ,[ConcurrencyCheck]
      ,[FinalInspectionComplete]
      ,[FinalCleanAndCheckComplete]
      ,[SecurityStatusId]
      ,[PlannedOnDemandHandoverComplete]
  FROM [CloudVoids].[dbo].[Void]' )"; }
        }

   }

}

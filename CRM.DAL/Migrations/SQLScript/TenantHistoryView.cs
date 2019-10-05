namespace CRM.DAL.Migrations.SQLScript
{
   public partial class SqlProgrammability    {

       public static string Drop_TenantHistoryView
        {
           get { return @"IF  EXISTS (SELECT * FROM sys.views
                                  WHERE object_id = OBJECT_ID(N'dbo.TenantHistoryView'))
                                  DROP VIEW dbo.TenantHistoryView"; } 
       }


        public static string Create_TenantHistoryView
        {
            get { return @"EXEC ('Create View [dbo].[TenantHistoryView] AS 
                        SELECT [PROPERTYREF]					AS PropertyCode
                              ,[TENANCYREF]						AS TenancyReference
                              ,[TENANCYPERSONREF]				AS TenancyPersonReference
                              ,[TENANCYTYPE]					AS TenancyType
                              ,[TENANCYSTATUS]					AS TenancyStatus
                              ,[CURRENTBALANCE]					AS CurrentBalance
                              ,[RENTGROUP]						AS RentGroup
                              ,[RENTGROUPDESCRIPTION]			AS RentGroupDescription
                              ,[TENANCYSTART]					AS TenancyStartDate
                              ,[TENANCYEND]						AS TenancyEndDate
                              ,[PERSONSTART]					AS PersonStartDate
                              ,[PERSONEND]						AS PersonEndDate
                              ,[JOINT_TENANT]					AS IsJointTenant
                              ,[MAIN_TENANT_FLAG]				AS IsMainTenant
                              ,[PERSON-REF]						AS TenantCode
                              ,[TENANT_DOB]						AS DateOfBirth
                              ,[TENANT_AGE]						AS Age
                              ,[TENANT_TITLE]					AS Title
                              ,[TENANT_FIRST_NAME]				AS FirstName
                              ,[TENANT_LAST_NAME]				AS LastName
                              ,[CONTACT_TELEPHONE_NUMBER]		AS TelephoneNUmber
                              ,[CONTACT_MOBILE_NUMBER]			AS MobileNumber
                              ,[TENANT_GENDER]					AS Gender
                              ,[TENANT_ETHNICITY]				AS Ehhnicity
                        FROM [MasterReferenceData].[dbo].[TBL_CUSTOMER_HISTORIC]' )"; }
        }

   }

}

namespace CRM.DAL.Migrations.SQLScript
{
   public partial class SqlProgrammability    {

       public static string Drop_IdentityUserView
        {
           get { return @"IF  EXISTS (SELECT * FROM sys.views
                                  WHERE object_id = OBJECT_ID(N'dbo.IdentityUserView'))
                                  DROP VIEW dbo.IdentityUserView"; } 
       }


        public static string Create_IdentityUserView
        {
            get { return @"EXEC ('Create View [dbo].[IdentityUserView] AS 
                        SELECT  u.[Id]
                              ,u.[FirstName]
                              ,u.[LastName]
                              ,u.[PersonId]
                              ,u.[Email]
                              ,u.[UserName]
	                          ,(SELECT STUFF((SELECT ''; '' + claim.ClaimValue 
                                  FROM [Identity].[dbo].[AspNetUserClaims] claim
                                  WHERE claim.UserId = iu.Id
		                          AND claim.ClaimType =''role'' 
                                  FOR XML PATH('''')), 1, 1, '''') Roles
                        FROM [Identity].[dbo].[AspNetUsers] iu
                        WHERE iu.Id= u.Id
                         )  Roles
                          FROM [Identity].[dbo].[AspNetUsers] u
' )"; }
        }

   }

}

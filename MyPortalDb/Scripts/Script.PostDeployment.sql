/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'dbo.FK_Person_User')
   AND parent_object_id = OBJECT_ID(N'dbo.People_Persons')
)
  ALTER TABLE [dbo].[People_Persons] DROP CONSTRAINT [FK_Person_User]

GO

ALTER TABLE [dbo].[People_Persons]
ADD CONSTRAINT [FK_Person_User]
FOREIGN KEY (UserId) REFERENCES [dbo].[AspNetUsers](Id)
ON DELETE SET NULL
ON UPDATE CASCADE

--Other Scripts
:r .\Script.SeedSystemData.sql

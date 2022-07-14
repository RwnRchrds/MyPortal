-- =============================================
-- Author:		R Richards
-- Create date: 14 Jul 2022
-- Description:	Used to get the default name of a person
-- =============================================
CREATE FUNCTION [dbo].[GetName_Default]
(
	-- Add the parameters for the function here
	@PersonId uniqueidentifier,
	@UsePreferred bit,
	@IncludeMiddleName bit
)
RETURNS TABLE
AS
RETURN
(
    -- Format: Bloggs, Joe Thomas
	-- Add the SELECT statement with parameter references here
    SELECT P.Id as [PersonId], CONCAT(IIF(@UsePreferred = 1, COALESCE(P.PreferredLastName, P.LastName), P.LastName),
        ', ', IIF(@UsePreferred = 1, COALESCE(P.PreferredFirstName, P.FirstName), P.FirstName),
        IIF(@IncludeMiddleName = 1, IIF(P.MiddleName IS NOT NULL, CONCAT(' ', P.MiddleName), ''), '')) as [Name]
    FROM People P
    WHERE P.Id = @PersonId
)
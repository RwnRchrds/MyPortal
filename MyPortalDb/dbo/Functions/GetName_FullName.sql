-- =============================================
-- Author:		R Richards
-- Create date: 14 Jul 2022
-- Description:	Used to get the full name of a person
-- =============================================
CREATE FUNCTION GetName_FullName
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
    -- Format: Mr Joe Thomas Bloggs
	-- Add the SELECT statement with parameter references here
    SELECT P.Id as [PersonId], CONCAT(IIF(P.Title IS NOT NULL, CONCAT(P.Title, ' '), ''),
        IIF(@UsePreferred = 1, COALESCE(P.PreferredFirstName, P.FirstName), P.FirstName),
        IIF(@IncludeMiddleName = 1, IIF(P.MiddleName IS NOT NULL, CONCAT(' ', P.MiddleName, ' '), ' '), ' '),
        IIF(@UsePreferred = 1, COALESCE(P.PreferredLastName, P.LastName), P.LastName)) as [Name]
    FROM People P
    WHERE P.Id = @PersonId
)
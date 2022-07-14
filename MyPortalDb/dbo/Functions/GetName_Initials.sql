-- =============================================
-- Author:		R Richards
-- Create date: 14 Jul 2022
-- Description:	Used to get the initials of a person
-- =============================================
CREATE FUNCTION GetName_Initials
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
    -- Format: Joe Thomas Bloggs
	-- Add the SELECT statement with parameter references here
    SELECT P.Id as [PersonId], CONCAT(SUBSTRING(IIF(@UsePreferred = 1, COALESCE(P.PreferredFirstName, P.FirstName), P.FirstName), 1, 1),
        IIF(@IncludeMiddleName = 1, IIF(P.MiddleName IS NOT NULL, SUBSTRING(P.MiddleName, 1, 1), ''), ''),
        SUBSTRING(IIF(@UsePreferred = 1, COALESCE(P.PreferredLastName, P.LastName), P.LastName), 1, 1)) as [Name]
    FROM People P
    WHERE P.Id = @PersonId
)
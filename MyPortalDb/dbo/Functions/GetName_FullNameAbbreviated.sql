CREATE FUNCTION GetName_FullNameAbbreviated
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
    -- Format: Mr J T Bloggs
	-- Add the SELECT statement with parameter references here
    SELECT P.Id as [PersonId], CONCAT(IIF(P.Title IS NOT NULL, CONCAT(P.Title, ' '), ''),
        SUBSTRING(IIF(@UsePreferred = 1, COALESCE(P.PreferredFirstName, P.FirstName), P.FirstName), 1, 1),
        IIF(@IncludeMiddleName = 1, IIF(P.MiddleName IS NOT NULL, CONCAT(' ', SUBSTRING(P.MiddleName, 1, 1), ' '), ' '), ' '),
        IIF(@UsePreferred = 1, COALESCE(P.PreferredLastName, P.LastName), P.LastName)) as [Name]
    FROM People P
    WHERE P.Id = @PersonId
)
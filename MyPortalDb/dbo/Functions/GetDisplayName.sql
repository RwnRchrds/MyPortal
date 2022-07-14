-- =============================================
-- Author:		R Richards
-- Create date: 14 Jul 2022
-- Description:	Used to get the display name of a user
-- =============================================
CREATE FUNCTION GetDisplayName
(
	-- Add the parameters for the function here
	@UserId uniqueidentifier,
	@NameFormat int,
	@UsePreferred bit,
	@IncludeMiddleName bit
)
RETURNS TABLE
AS
RETURN
(
    -- Format: Joe Thomas Bloggs
	-- Add the SELECT statement with parameter references here
    SELECT U.Id as [UserId],
           CASE
               WHEN P.Id IS NOT NULL AND @NameFormat = 0 THEN (SELECT Name FROM GetName_Default(P.Id, @UsePreferred, @IncludeMiddleName))
               WHEN P.Id IS NOT NULL AND @NameFormat = 1 THEN (SELECT Name FROM GetName_FullName(P.Id, @UsePreferred, @IncludeMiddleName))
               WHEN P.Id IS NOT NULL AND @NameFormat = 2 THEN (SELECT Name FROM GetName_FullNameAbbreviated(P.Id, @UsePreferred, @IncludeMiddleName))
               WHEN P.Id IS NOT NULL AND @NameFormat = 3 THEN (SELECT Name FROM GetName_FullNameNoTitle(P.Id, @UsePreferred, @IncludeMiddleName))
               WHEN P.Id IS NOT NULL AND @NameFormat = 4 THEN (SELECT Name FROM GetName_Initials(P.Id, @UsePreferred, @IncludeMiddleName))
               ELSE U.UserName
           END
               as [DisplayName]
    FROM Users U
    LEFT JOIN People P on U.PersonId = P.Id
    WHERE U.Id = @UserId
)
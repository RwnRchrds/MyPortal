CREATE VIEW [dbo].[People_FullNameAbbreviated]
AS
SELECT        Id AS PersonId, REPLACE(ISNULL(Title, '') + ' ' + SUBSTRING(FirstName, 1, 1) + ' ' + ISNULL(MiddleName, '') + ' ' + LastName, '  ', ' ') AS Name
FROM            dbo.People AS P
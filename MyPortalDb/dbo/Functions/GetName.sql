CREATE FUNCTION GetName(
    @PersonId uniqueidentifier,
    @NameFormat int,
    @UsePreferredName bit,
    @IncludeMiddleName bit
)
    RETURNS TABLE
    AS
        RETURN SELECT P.Id,
                      -- NameFormat.FullName
                      CASE
                          WHEN @NameFormat = 1
                              THEN CONCAT(IIF(P.Title IS NOT NULL, CONCAT(P.Title, ' '), ''),
                                          IIF(@UsePreferredName = 1, COALESCE(P.PreferredFirstName, P.FirstName),
                                              P.FirstName),
                                          IIF(@IncludeMiddleName = 1,
                                              IIF(P.MiddleName IS NOT NULL, CONCAT(' ', P.MiddleName, ' '), ' '), ' '),
                                          IIF(@UsePreferredName = 1, COALESCE(P.PreferredLastName, P.LastName),
                                              P.LastName))

                              -- NameFormat.FullNameAbbreviated
                          WHEN @NameFormat = 2
                              THEN CONCAT(IIF(P.Title IS NOT NULL, CONCAT(P.Title, ' '), ''),
                                          SUBSTRING(IIF(@UsePreferredName = 1,
                                                        COALESCE(P.PreferredFirstName, P.FirstName), P.FirstName), 1,
                                                    1),
                                          IIF(@IncludeMiddleName = 1, IIF(P.MiddleName IS NOT NULL,
                                                                          CONCAT(' ', SUBSTRING(P.MiddleName, 1, 1), ' '),
                                                                          ' '), ' '),
                                          IIF(@UsePreferredName = 1, COALESCE(P.PreferredLastName, P.LastName),
                                              P.LastName))

                              -- NameFormat.FullNameNoTitle
                          WHEN @NameFormat = 3
                              THEN CONCAT(
                                  IIF(@UsePreferredName = 1, COALESCE(P.PreferredFirstName, P.FirstName), P.FirstName),
                                  IIF(@IncludeMiddleName = 1,
                                      IIF(P.MiddleName IS NOT NULL, CONCAT(' ', P.MiddleName, ' '), ' '), ' '),
                                  IIF(@UsePreferredName = 1, COALESCE(P.PreferredLastName, P.LastName), P.LastName))

                              -- NameFormat.Initials
                          WHEN @NameFormat = 4
                              THEN CONCAT(
                                  SUBSTRING(IIF(@UsePreferredName = 1, COALESCE(P.PreferredFirstName, P.FirstName), P.FirstName), 1, 1),
                                  IIF(@IncludeMiddleName = 1, IIF(P.MiddleName IS NOT NULL, SUBSTRING(P.MiddleName, 1, 1), ''), ' '),
                                  SUBSTRING(IIF(@UsePreferredName = 1, COALESCE(P.PreferredLastName, P.LastName), P.LastName), 1, 1)
                              )

                          ELSE
                              CONCAT(IIF(@UsePreferredName = 1, COALESCE(P.PreferredLastName, P.LastName), P.LastName),
                                     ', ',
                                     IIF(@UsePreferredName = 1, COALESCE(P.PreferredFirstName, P.FirstName), P.FirstName),
                                     IIF(@IncludeMiddleName = 1, IIF(P.MiddleName IS NOT NULL, CONCAT(' ', P.MiddleName), ''), '')
                                  )
                              END AS Name

               FROM dbo.People P

               WHERE P.Id = @PersonId;
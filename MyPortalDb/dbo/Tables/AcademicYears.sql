CREATE TABLE [dbo].[AcademicYears] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (128)   NOT NULL,
    [Locked]    BIT              NOT NULL,
    CONSTRAINT [PK_AcademicYears] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AcademicYears]([ClusterId] ASC);


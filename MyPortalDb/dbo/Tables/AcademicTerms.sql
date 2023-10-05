CREATE TABLE [dbo].[AcademicTerms] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [AcademicYearId] UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (128)   NULL,
    [StartDate]      DATE             NOT NULL,
    [EndDate]        DATE             NOT NULL,
    CONSTRAINT [PK_AcademicTerms] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AcademicTerms_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AcademicTerms]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AcademicTerms_AcademicYearId]
    ON [dbo].[AcademicTerms]([AcademicYearId] ASC);


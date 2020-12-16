CREATE TABLE [dbo].[AcademicTerms] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AcademicYearId] UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (128)   NULL,
    [StartDate]      DATETIME2 (7)    NOT NULL,
    [EndDate]        DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_AcademicTerms] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AcademicTerms_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AcademicTerms_AcademicYearId]
    ON [dbo].[AcademicTerms]([AcademicYearId] ASC);


CREATE TABLE [dbo].[CurriculumBands] (
    [Id]                    UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AcademicYearId]        UNIQUEIDENTIFIER NOT NULL,
    [CurriculumYearGroupId] UNIQUEIDENTIFIER NOT NULL,
    [Code]                  NVARCHAR (10)    NOT NULL,
    [Description]           NVARCHAR (256)   NULL,
    CONSTRAINT [PK_CurriculumBands] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumBands_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CurriculumBands_CurriculumYearGroups_CurriculumYearGroupId] FOREIGN KEY ([CurriculumYearGroupId]) REFERENCES [dbo].[CurriculumYearGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumBands_CurriculumYearGroupId]
    ON [dbo].[CurriculumBands]([CurriculumYearGroupId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CurriculumBands_AcademicYearId_Code]
    ON [dbo].[CurriculumBands]([AcademicYearId] ASC, [Code] ASC);


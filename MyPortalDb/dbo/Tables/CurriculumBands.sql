CREATE TABLE [dbo].[CurriculumBands] (
    [Id]                    UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AcademicYearId]        UNIQUEIDENTIFIER NOT NULL,
    [CurriculumYearGroupId] UNIQUEIDENTIFIER NOT NULL,
    [StudentGroupId]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CurriculumBands] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumBands_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CurriculumBands_CurriculumYearGroups_CurriculumYearGroupId] FOREIGN KEY ([CurriculumYearGroupId]) REFERENCES [dbo].[CurriculumYearGroups] ([Id]),
    CONSTRAINT [FK_CurriculumBands_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumBands_AcademicYearId]
    ON [dbo].[CurriculumBands]([AcademicYearId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumBands_CurriculumYearGroupId]
    ON [dbo].[CurriculumBands]([CurriculumYearGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumBands_StudentGroupId]
    ON [dbo].[CurriculumBands]([StudentGroupId] ASC);


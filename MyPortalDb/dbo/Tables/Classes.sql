CREATE TABLE [dbo].[Classes] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [CourseId]       UNIQUEIDENTIFIER NOT NULL,
    [GroupId]        UNIQUEIDENTIFIER NOT NULL,
    [Code]           NVARCHAR (10)    NOT NULL,
    [AcademicYearId] UNIQUEIDENTIFIER NULL,
    [YearGroupId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Classes_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id]),
    CONSTRAINT [FK_Classes_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]),
    CONSTRAINT [FK_Classes_CurriculumGroups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[CurriculumGroups] ([Id]),
    CONSTRAINT [FK_Classes_YearGroups_YearGroupId] FOREIGN KEY ([YearGroupId]) REFERENCES [dbo].[YearGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_AcademicYearId]
    ON [dbo].[Classes]([AcademicYearId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_CourseId]
    ON [dbo].[Classes]([CourseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_GroupId]
    ON [dbo].[Classes]([GroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Classes_YearGroupId]
    ON [dbo].[Classes]([YearGroupId] ASC);


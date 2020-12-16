CREATE TABLE [dbo].[Achievements] (
    [Id]                UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AcademicYearId]    UNIQUEIDENTIFIER NOT NULL,
    [AchievementTypeId] UNIQUEIDENTIFIER NOT NULL,
    [StudentId]         UNIQUEIDENTIFIER NOT NULL,
    [LocationId]        UNIQUEIDENTIFIER NULL,
    [RecordedById]      UNIQUEIDENTIFIER NOT NULL,
    [OutcomeId]         UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]       DATE             NOT NULL,
    [Comments]          NVARCHAR (MAX)   NULL,
    [Points]            INT              NOT NULL,
    [Deleted]           BIT              NOT NULL,
    CONSTRAINT [PK_Achievements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Achievements_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id]),
    CONSTRAINT [FK_Achievements_AchievementOutcomes_OutcomeId] FOREIGN KEY ([OutcomeId]) REFERENCES [dbo].[AchievementOutcomes] ([Id]),
    CONSTRAINT [FK_Achievements_AchievementTypes_AchievementTypeId] FOREIGN KEY ([AchievementTypeId]) REFERENCES [dbo].[AchievementTypes] ([Id]),
    CONSTRAINT [FK_Achievements_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id]),
    CONSTRAINT [FK_Achievements_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_Achievements_Users_RecordedById] FOREIGN KEY ([RecordedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_AcademicYearId]
    ON [dbo].[Achievements]([AcademicYearId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_AchievementTypeId]
    ON [dbo].[Achievements]([AchievementTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_LocationId]
    ON [dbo].[Achievements]([LocationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_OutcomeId]
    ON [dbo].[Achievements]([OutcomeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_RecordedById]
    ON [dbo].[Achievements]([RecordedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_StudentId]
    ON [dbo].[Achievements]([StudentId] ASC);


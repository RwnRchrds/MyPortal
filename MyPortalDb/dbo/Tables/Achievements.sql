CREATE TABLE [dbo].[Achievements] (
    [Id]                UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AcademicYearId]    UNIQUEIDENTIFIER NOT NULL,
    [AchievementTypeId] UNIQUEIDENTIFIER NOT NULL,
    [LocationId]        UNIQUEIDENTIFIER NULL,
    [CreatedById]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]       DATETIME2 (7)    NOT NULL,
    [Date]              DATE             NOT NULL,
    [Comments]          NVARCHAR (MAX)   NULL,
    [Deleted]           BIT              NOT NULL,
    CONSTRAINT [PK_Achievements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Achievements_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id]),
    CONSTRAINT [FK_Achievements_AchievementTypes_AchievementTypeId] FOREIGN KEY ([AchievementTypeId]) REFERENCES [dbo].[AchievementTypes] ([Id]),
    CONSTRAINT [FK_Achievements_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id]),
    CONSTRAINT [FK_Achievements_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_AcademicYearId]
    ON [dbo].[Achievements]([AcademicYearId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_AchievementTypeId]
    ON [dbo].[Achievements]([AchievementTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_CreatedById]
    ON [dbo].[Achievements]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Achievements_LocationId]
    ON [dbo].[Achievements]([LocationId] ASC);


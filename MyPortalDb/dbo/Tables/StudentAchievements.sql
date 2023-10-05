CREATE TABLE [dbo].[StudentAchievements] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]     UNIQUEIDENTIFIER NOT NULL,
    [AchievementId] UNIQUEIDENTIFIER NOT NULL,
    [OutcomeId]     UNIQUEIDENTIFIER NOT NULL,
    [Points]        INT              NOT NULL,
    CONSTRAINT [PK_StudentAchievements] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentAchievements_AchievementOutcomes_OutcomeId] FOREIGN KEY ([OutcomeId]) REFERENCES [dbo].[AchievementOutcomes] ([Id]),
    CONSTRAINT [FK_StudentAchievements_Achievements_AchievementId] FOREIGN KEY ([AchievementId]) REFERENCES [dbo].[Achievements] ([Id]),
    CONSTRAINT [FK_StudentAchievements_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StudentAchievements]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentAchievements_AchievementId]
    ON [dbo].[StudentAchievements]([AchievementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentAchievements_OutcomeId]
    ON [dbo].[StudentAchievements]([OutcomeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentAchievements_StudentId]
    ON [dbo].[StudentAchievements]([StudentId] ASC);


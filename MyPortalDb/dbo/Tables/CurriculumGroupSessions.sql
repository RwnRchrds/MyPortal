CREATE TABLE [dbo].[CurriculumGroupSessions] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]         INT              IDENTITY (1, 1) NOT NULL,
    [CurriculumGroupId] UNIQUEIDENTIFIER NOT NULL,
    [SubjectId]         UNIQUEIDENTIFIER NOT NULL,
    [SessionTypeId]     UNIQUEIDENTIFIER NOT NULL,
    [SessionAmount]     INT              NOT NULL,
    CONSTRAINT [PK_CurriculumGroupSessions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumGroupSessions_CurriculumGroups_CurriculumGroupId] FOREIGN KEY ([CurriculumGroupId]) REFERENCES [dbo].[CurriculumGroups] ([Id]),
    CONSTRAINT [FK_CurriculumGroupSessions_SessionTypes_SessionTypeId] FOREIGN KEY ([SessionTypeId]) REFERENCES [dbo].[SessionTypes] ([Id]),
    CONSTRAINT [FK_CurriculumGroupSessions_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[CurriculumGroupSessions]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroupSessions_CurriculumGroupId]
    ON [dbo].[CurriculumGroupSessions]([CurriculumGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroupSessions_SessionTypeId]
    ON [dbo].[CurriculumGroupSessions]([SessionTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroupSessions_SubjectId]
    ON [dbo].[CurriculumGroupSessions]([SubjectId] ASC);


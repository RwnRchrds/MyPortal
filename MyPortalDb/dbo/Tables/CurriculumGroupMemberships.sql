CREATE TABLE [dbo].[CurriculumGroupMemberships] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [GroupId]   UNIQUEIDENTIFIER NOT NULL,
    [StartDate] DATETIME2 (7)    NOT NULL,
    [EndDate]   DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_CurriculumGroupMemberships] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumGroupMemberships_CurriculumGroups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[CurriculumGroups] ([Id]),
    CONSTRAINT [FK_CurriculumGroupMemberships_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroupMemberships_GroupId]
    ON [dbo].[CurriculumGroupMemberships]([GroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroupMemberships_StudentId]
    ON [dbo].[CurriculumGroupMemberships]([StudentId] ASC);


CREATE TABLE [dbo].[CurriculumGroups] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BlockId]        UNIQUEIDENTIFIER NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CurriculumGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumGroups_CurriculumBlocks_BlockId] FOREIGN KEY ([BlockId]) REFERENCES [dbo].[CurriculumBlocks] ([Id]),
    CONSTRAINT [FK_CurriculumGroups_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroups_BlockId]
    ON [dbo].[CurriculumGroups]([BlockId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroups_StudentGroupId]
    ON [dbo].[CurriculumGroups]([StudentGroupId] ASC);


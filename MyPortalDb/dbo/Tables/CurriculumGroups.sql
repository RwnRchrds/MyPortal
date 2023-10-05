CREATE TABLE [dbo].[CurriculumGroups] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [BlockId]        UNIQUEIDENTIFIER NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CurriculumGroups] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumGroups_CurriculumBlocks_BlockId] FOREIGN KEY ([BlockId]) REFERENCES [dbo].[CurriculumBlocks] ([Id]),
    CONSTRAINT [FK_CurriculumGroups_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[CurriculumGroups]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroups_BlockId]
    ON [dbo].[CurriculumGroups]([BlockId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroups_StudentGroupId]
    ON [dbo].[CurriculumGroups]([StudentGroupId] ASC);


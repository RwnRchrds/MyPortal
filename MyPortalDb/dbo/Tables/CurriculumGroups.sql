CREATE TABLE [dbo].[CurriculumGroups] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BlockId]     UNIQUEIDENTIFIER NOT NULL,
    [Code]        NVARCHAR (10)    NULL,
    [Description] NVARCHAR (256)   NULL,
    CONSTRAINT [PK_CurriculumGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumGroups_CurriculumBlocks_BlockId] FOREIGN KEY ([BlockId]) REFERENCES [dbo].[CurriculumBlocks] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumGroups_BlockId]
    ON [dbo].[CurriculumGroups]([BlockId] ASC);


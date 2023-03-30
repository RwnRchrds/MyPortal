CREATE TABLE [dbo].[CurriculumBandBlockAssignments] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [BlockId]   UNIQUEIDENTIFIER NOT NULL,
    [BandId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CurriculumBandBlockAssignments] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CurriculumBandBlockAssignments_CurriculumBands_BandId] FOREIGN KEY ([BandId]) REFERENCES [dbo].[CurriculumBands] ([Id]),
    CONSTRAINT [FK_CurriculumBandBlockAssignments_CurriculumBlocks_BlockId] FOREIGN KEY ([BlockId]) REFERENCES [dbo].[CurriculumBlocks] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_CurriculumBandBlockAssignments_BandId]
    ON [dbo].[CurriculumBandBlockAssignments]([BandId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumBandBlockAssignments_BlockId]
    ON [dbo].[CurriculumBandBlockAssignments]([BlockId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[CurriculumBandBlockAssignments]([ClusterId] ASC);


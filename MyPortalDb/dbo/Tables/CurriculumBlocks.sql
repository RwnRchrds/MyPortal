CREATE TABLE [dbo].[CurriculumBlocks] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (10)    NULL,
    [Description] NVARCHAR (256)   NULL,
    CONSTRAINT [PK_CurriculumBlocks] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[CurriculumBlocks]([ClusterId] ASC);


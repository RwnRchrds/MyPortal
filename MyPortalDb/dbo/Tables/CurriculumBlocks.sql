CREATE TABLE [dbo].[CurriculumBlocks] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code]        NVARCHAR (10)    NULL,
    [Description] NVARCHAR (256)   NULL,
    CONSTRAINT [PK_CurriculumBlocks] PRIMARY KEY CLUSTERED ([Id] ASC)
);


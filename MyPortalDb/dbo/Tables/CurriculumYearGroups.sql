CREATE TABLE [dbo].[CurriculumYearGroups] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (128)   NOT NULL,
    [KeyStage]  INT              NOT NULL,
    [Code]      NVARCHAR (10)    NOT NULL,
    CONSTRAINT [PK_CurriculumYearGroups] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[CurriculumYearGroups]([ClusterId] ASC);


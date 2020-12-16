CREATE TABLE [dbo].[CurriculumYearGroups] (
    [Id]       UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]     NVARCHAR (128)   NOT NULL,
    [KeyStage] INT              NOT NULL,
    [Code]     NVARCHAR (10)    NOT NULL,
    CONSTRAINT [PK_CurriculumYearGroups] PRIMARY KEY CLUSTERED ([Id] ASC)
);


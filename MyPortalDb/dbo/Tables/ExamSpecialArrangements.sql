CREATE TABLE [dbo].[ExamSpecialArrangements] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_ExamSpecialArrangements] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[ExamSpecialArrangements] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_ExamSpecialArrangements] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamSpecialArrangements]([ClusterId] ASC);


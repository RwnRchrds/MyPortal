CREATE TABLE [dbo].[ExamResultEmbargoes] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [ResultSetId] UNIQUEIDENTIFIER NOT NULL,
    [EndTime]     DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ExamResultEmbargoes] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamResultEmbargoes_ResultSets_ResultSetId] FOREIGN KEY ([ResultSetId]) REFERENCES [dbo].[ResultSets] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamResultEmbargoes]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamResultEmbargoes_ResultSetId]
    ON [dbo].[ExamResultEmbargoes]([ResultSetId] ASC);


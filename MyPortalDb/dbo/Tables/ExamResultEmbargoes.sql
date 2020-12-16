CREATE TABLE [dbo].[ExamResultEmbargoes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ResultSetId] UNIQUEIDENTIFIER NOT NULL,
    [StartTime]   DATETIME2 (7)    NOT NULL,
    [EndTime]     DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ExamResultEmbargoes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamResultEmbargoes_ResultSets_ResultSetId] FOREIGN KEY ([ResultSetId]) REFERENCES [dbo].[ResultSets] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamResultEmbargoes_ResultSetId]
    ON [dbo].[ExamResultEmbargoes]([ResultSetId] ASC);


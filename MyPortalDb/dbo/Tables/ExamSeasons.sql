CREATE TABLE [dbo].[ExamSeasons] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ResultSetId]  UNIQUEIDENTIFIER NOT NULL,
    [CalendarYear] INT              NOT NULL,
    [StartDate]    DATETIME2 (7)    NOT NULL,
    [EndDate]      DATETIME2 (7)    NOT NULL,
    [Name]         NVARCHAR (MAX)   NULL,
    [Description]  NVARCHAR (MAX)   NULL,
    [Default]      BIT              NOT NULL,
    CONSTRAINT [PK_ExamSeasons] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamSeasons_ResultSets_ResultSetId] FOREIGN KEY ([ResultSetId]) REFERENCES [dbo].[ResultSets] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamSeasons_ResultSetId]
    ON [dbo].[ExamSeasons]([ResultSetId] ASC);


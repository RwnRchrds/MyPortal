CREATE TABLE [dbo].[ExamSeasons] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [ResultSetId]  UNIQUEIDENTIFIER NOT NULL,
    [CalendarYear] INT              NOT NULL,
    [StartDate]    DATE             NOT NULL,
    [EndDate]      DATE             NOT NULL,
    [Name]         NVARCHAR (MAX)   NULL,
    [Description]  NVARCHAR (MAX)   NULL,
    [Default]      BIT              NOT NULL,
    CONSTRAINT [PK_ExamSeasons] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamSeasons_ResultSets_ResultSetId] FOREIGN KEY ([ResultSetId]) REFERENCES [dbo].[ResultSets] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ExamSeasons_ResultSetId]
    ON [dbo].[ExamSeasons]([ResultSetId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamSeasons]([ClusterId] ASC);


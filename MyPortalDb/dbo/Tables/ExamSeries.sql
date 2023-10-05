CREATE TABLE [dbo].[ExamSeries] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [ExamBoardId]  UNIQUEIDENTIFIER NOT NULL,
    [ExamSeasonId] UNIQUEIDENTIFIER NOT NULL,
    [SeriesCode]   NVARCHAR (MAX)   NULL,
    [Code]         NVARCHAR (MAX)   NULL,
    [Title]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamSeries] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamSeries_ExamBoards_ExamBoardId] FOREIGN KEY ([ExamBoardId]) REFERENCES [dbo].[ExamBoards] ([Id]),
    CONSTRAINT [FK_ExamSeries_ExamSeasons_ExamSeasonId] FOREIGN KEY ([ExamSeasonId]) REFERENCES [dbo].[ExamSeasons] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamSeries]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamSeries_ExamBoardId]
    ON [dbo].[ExamSeries]([ExamBoardId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamSeries_ExamSeasonId]
    ON [dbo].[ExamSeries]([ExamSeasonId] ASC);


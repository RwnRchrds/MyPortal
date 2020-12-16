CREATE TABLE [dbo].[ExamAwardSeries] (
    [Id]       UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AwardId]  UNIQUEIDENTIFIER NOT NULL,
    [SeriesId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ExamAwardSeries] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamAwardSeries_ExamAwards_AwardId] FOREIGN KEY ([AwardId]) REFERENCES [dbo].[ExamAwards] ([Id]),
    CONSTRAINT [FK_ExamAwardSeries_ExamSeries_AwardId] FOREIGN KEY ([AwardId]) REFERENCES [dbo].[ExamSeries] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAwardSeries_AwardId]
    ON [dbo].[ExamAwardSeries]([AwardId] ASC);


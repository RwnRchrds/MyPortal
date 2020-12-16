CREATE TABLE [dbo].[ExamCandidateSeries] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [SeriesId]    UNIQUEIDENTIFIER NOT NULL,
    [CandidateId] UNIQUEIDENTIFIER NOT NULL,
    [Flag]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamCandidateSeries] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamCandidateSeries_ExamCandidate_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[ExamCandidate] ([Id]),
    CONSTRAINT [FK_ExamCandidateSeries_ExamSeries_SeriesId] FOREIGN KEY ([SeriesId]) REFERENCES [dbo].[ExamSeries] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamCandidateSeries_CandidateId]
    ON [dbo].[ExamCandidateSeries]([CandidateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamCandidateSeries_SeriesId]
    ON [dbo].[ExamCandidateSeries]([SeriesId] ASC);


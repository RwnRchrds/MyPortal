CREATE TABLE [dbo].[ExamComponents] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BaseComponentId]  UNIQUEIDENTIFIER NOT NULL,
    [ExamSeriesId]     UNIQUEIDENTIFIER NOT NULL,
    [AssessmentModeId] UNIQUEIDENTIFIER NOT NULL,
    [ExamDateId]       UNIQUEIDENTIFIER NULL,
    [DateDue]          DATETIME2 (7)    NULL,
    [DateSubmitted]    DATETIME2 (7)    NULL,
    [MaximumMark]      INT              NOT NULL,
    CONSTRAINT [PK_ExamComponents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamComponents_ExamAssessmentModes_AssessmentModeId] FOREIGN KEY ([AssessmentModeId]) REFERENCES [dbo].[ExamAssessmentModes] ([Id]),
    CONSTRAINT [FK_ExamComponents_ExamBaseComponents_BaseComponentId] FOREIGN KEY ([BaseComponentId]) REFERENCES [dbo].[ExamBaseComponents] ([Id]),
    CONSTRAINT [FK_ExamComponents_ExamDates_ExamDateId] FOREIGN KEY ([ExamDateId]) REFERENCES [dbo].[ExamDates] ([Id]),
    CONSTRAINT [FK_ExamComponents_ExamSeries_ExamSeriesId] FOREIGN KEY ([ExamSeriesId]) REFERENCES [dbo].[ExamSeries] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamComponents_AssessmentModeId]
    ON [dbo].[ExamComponents]([AssessmentModeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamComponents_BaseComponentId]
    ON [dbo].[ExamComponents]([BaseComponentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamComponents_ExamDateId]
    ON [dbo].[ExamComponents]([ExamDateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamComponents_ExamSeriesId]
    ON [dbo].[ExamComponents]([ExamSeriesId] ASC);


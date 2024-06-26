﻿CREATE TABLE [dbo].[ExamAssessmentAspects] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [AssessmentId] UNIQUEIDENTIFIER NOT NULL,
    [AspectId]     UNIQUEIDENTIFIER NOT NULL,
    [SeriesId]     UNIQUEIDENTIFIER NOT NULL,
    [AspectOrder]  INT              NOT NULL,
    CONSTRAINT [PK_ExamAssessmentAspects] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamAssessmentAspects_Aspects_AspectId] FOREIGN KEY ([AspectId]) REFERENCES [dbo].[Aspects] ([Id]),
    CONSTRAINT [FK_ExamAssessmentAspects_ExamAssessments_AssessmentId] FOREIGN KEY ([AssessmentId]) REFERENCES [dbo].[ExamAssessments] ([Id]),
    CONSTRAINT [FK_ExamAssessmentAspects_ExamSeries_SeriesId] FOREIGN KEY ([SeriesId]) REFERENCES [dbo].[ExamSeries] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamAssessmentAspects]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAssessmentAspects_AspectId]
    ON [dbo].[ExamAssessmentAspects]([AspectId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAssessmentAspects_AssessmentId]
    ON [dbo].[ExamAssessmentAspects]([AssessmentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAssessmentAspects_SeriesId]
    ON [dbo].[ExamAssessmentAspects]([SeriesId] ASC);


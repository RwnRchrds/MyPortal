CREATE TABLE [dbo].[ExamBaseComponents] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]        INT              IDENTITY (1, 1) NOT NULL,
    [AssessmentModeId] UNIQUEIDENTIFIER NOT NULL,
    [ExamAssessmentId] UNIQUEIDENTIFIER NOT NULL,
    [ComponentCode]    NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamBaseComponents] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamBaseComponents_ExamAssessmentModes_AssessmentModeId] FOREIGN KEY ([AssessmentModeId]) REFERENCES [dbo].[ExamAssessmentModes] ([Id]),
    CONSTRAINT [FK_ExamBaseComponents_ExamAssessments_ExamAssessmentId] FOREIGN KEY ([ExamAssessmentId]) REFERENCES [dbo].[ExamAssessments] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamBaseComponents]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamBaseComponents_AssessmentModeId]
    ON [dbo].[ExamBaseComponents]([AssessmentModeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamBaseComponents_ExamAssessmentId]
    ON [dbo].[ExamBaseComponents]([ExamAssessmentId] ASC);


CREATE TABLE [dbo].[ExamBaseElements] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [AssessmentId]   UNIQUEIDENTIFIER NOT NULL,
    [LevelId]        UNIQUEIDENTIFIER NOT NULL,
    [QcaCodeId]      UNIQUEIDENTIFIER NOT NULL,
    [QualAccrNumber] NVARCHAR (MAX)   NULL,
    [ElementCode]    NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamBaseElements] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamBaseElements_ExamAssessments_AssessmentId] FOREIGN KEY ([AssessmentId]) REFERENCES [dbo].[ExamAssessments] ([Id]),
    CONSTRAINT [FK_ExamBaseElements_ExamQualificationLevels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [dbo].[ExamQualificationLevels] ([Id]),
    CONSTRAINT [FK_ExamBaseElements_SubjectCodes_QcaCodeId] FOREIGN KEY ([QcaCodeId]) REFERENCES [dbo].[SubjectCodes] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamBaseElements]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamBaseElements_AssessmentId]
    ON [dbo].[ExamBaseElements]([AssessmentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamBaseElements_LevelId]
    ON [dbo].[ExamBaseElements]([LevelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamBaseElements_QcaCodeId]
    ON [dbo].[ExamBaseElements]([QcaCodeId] ASC);


CREATE TABLE [dbo].[ExamAwards] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]       INT              IDENTITY (1, 1) NOT NULL,
    [QualificationId] UNIQUEIDENTIFIER NOT NULL,
    [AssessmentId]    UNIQUEIDENTIFIER NOT NULL,
    [CourseId]        UNIQUEIDENTIFIER NULL,
    [Description]     NVARCHAR (MAX)   NULL,
    [AwardCode]       NVARCHAR (MAX)   NULL,
    [ExpiryDate]      DATE             NULL,
    CONSTRAINT [PK_ExamAwards] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamAwards_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]),
    CONSTRAINT [FK_ExamAwards_ExamAssessments_AssessmentId] FOREIGN KEY ([AssessmentId]) REFERENCES [dbo].[ExamAssessments] ([Id]),
    CONSTRAINT [FK_ExamAwards_ExamQualifications_QualificationId] FOREIGN KEY ([QualificationId]) REFERENCES [dbo].[ExamQualifications] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ExamAwards_AssessmentId]
    ON [dbo].[ExamAwards]([AssessmentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAwards_CourseId]
    ON [dbo].[ExamAwards]([CourseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAwards_QualificationId]
    ON [dbo].[ExamAwards]([QualificationId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamAwards]([ClusterId] ASC);


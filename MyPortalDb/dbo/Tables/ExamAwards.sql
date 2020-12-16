CREATE TABLE [dbo].[ExamAwards] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [QualificationId] UNIQUEIDENTIFIER NOT NULL,
    [AssessmentId]    UNIQUEIDENTIFIER NOT NULL,
    [CourseId]        UNIQUEIDENTIFIER NULL,
    [Description]     NVARCHAR (MAX)   NULL,
    [AwardCode]       NVARCHAR (MAX)   NULL,
    [ExpiryDate]      DATETIME2 (7)    NULL,
    CONSTRAINT [PK_ExamAwards] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamAwards_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_ExamAwards_ExamAssessments_AssessmentId] FOREIGN KEY ([AssessmentId]) REFERENCES [dbo].[ExamAssessments] ([Id]),
    CONSTRAINT [FK_ExamAwards_ExamQualifications_QualificationId] FOREIGN KEY ([QualificationId]) REFERENCES [dbo].[ExamQualifications] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ExamAwards_AssessmentId]
    ON [dbo].[ExamAwards]([AssessmentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAwards_CourseId]
    ON [dbo].[ExamAwards]([CourseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAwards_QualificationId]
    ON [dbo].[ExamAwards]([QualificationId] ASC);


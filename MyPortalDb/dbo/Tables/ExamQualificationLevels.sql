CREATE TABLE [dbo].[ExamQualificationLevels] (
    [Id]                UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description]       NVARCHAR (256)   NOT NULL,
    [Active]            BIT              NOT NULL,
    [QualificationId]   UNIQUEIDENTIFIER NOT NULL,
    [DefaultGradeSetId] UNIQUEIDENTIFIER NULL,
    [JcLevelCode]       NVARCHAR (25)    NULL,
    CONSTRAINT [PK_ExamQualificationLevels] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamQualificationLevels_ExamQualifications_QualificationId] FOREIGN KEY ([QualificationId]) REFERENCES [dbo].[ExamQualifications] ([Id]),
    CONSTRAINT [FK_ExamQualificationLevels_GradeSets_DefaultGradeSetId] FOREIGN KEY ([DefaultGradeSetId]) REFERENCES [dbo].[GradeSets] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamQualificationLevels_DefaultGradeSetId]
    ON [dbo].[ExamQualificationLevels]([DefaultGradeSetId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamQualificationLevels_QualificationId]
    ON [dbo].[ExamQualificationLevels]([QualificationId] ASC);


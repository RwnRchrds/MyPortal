CREATE TABLE [dbo].[ExamQualificationLevels] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]         INT              IDENTITY (1, 1) NOT NULL,
    [Description]       NVARCHAR (256)   NOT NULL,
    [Active]            BIT              NOT NULL,
    [QualificationId]   UNIQUEIDENTIFIER NOT NULL,
    [DefaultGradeSetId] UNIQUEIDENTIFIER NULL,
    [JcLevelCode]       NVARCHAR (25)    NULL,
    [System]            BIT              NOT NULL,
    CONSTRAINT [PK_ExamQualificationLevels] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamQualificationLevels_ExamQualifications_QualificationId] FOREIGN KEY ([QualificationId]) REFERENCES [dbo].[ExamQualifications] ([Id]),
    CONSTRAINT [FK_ExamQualificationLevels_GradeSets_DefaultGradeSetId] FOREIGN KEY ([DefaultGradeSetId]) REFERENCES [dbo].[GradeSets] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamQualificationLevels]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamQualificationLevels_DefaultGradeSetId]
    ON [dbo].[ExamQualificationLevels]([DefaultGradeSetId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamQualificationLevels_QualificationId]
    ON [dbo].[ExamQualificationLevels]([QualificationId] ASC);


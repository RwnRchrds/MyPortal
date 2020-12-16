CREATE TABLE [dbo].[ExamAssessmentModes] (
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description]        NVARCHAR (256)   NOT NULL,
    [Active]             BIT              NOT NULL,
    [ExternallyAssessed] BIT              NOT NULL,
    CONSTRAINT [PK_ExamAssessmentModes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


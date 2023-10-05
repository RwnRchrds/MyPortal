CREATE TABLE [dbo].[ExamAssessmentModes] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]          INT              IDENTITY (1, 1) NOT NULL,
    [Description]        NVARCHAR (256)   NOT NULL,
    [Active]             BIT              NOT NULL,
    [ExternallyAssessed] BIT              NOT NULL,
    CONSTRAINT [PK_ExamAssessmentModes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamAssessmentModes]([ClusterId] ASC);


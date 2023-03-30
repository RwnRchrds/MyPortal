CREATE TABLE [dbo].[ExamAssessments] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [ExamBoardId]    UNIQUEIDENTIFIER NOT NULL,
    [AssessmentType] INT              NOT NULL,
    [InternalTitle]  NVARCHAR (MAX)   NULL,
    [ExternalTitle]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ExamAssessments] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamAssessments_ExamBoards_ExamBoardId] FOREIGN KEY ([ExamBoardId]) REFERENCES [dbo].[ExamBoards] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ExamAssessments_ExamBoardId]
    ON [dbo].[ExamAssessments]([ExamBoardId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamAssessments]([ClusterId] ASC);


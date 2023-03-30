CREATE TABLE [dbo].[ExamAwardElements] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [AwardId]   UNIQUEIDENTIFIER NOT NULL,
    [ElementId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ExamAwardElements] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamAwardElements_ExamAwards_AwardId] FOREIGN KEY ([AwardId]) REFERENCES [dbo].[ExamAwards] ([Id]),
    CONSTRAINT [FK_ExamAwardElements_ExamElements_ElementId] FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ExamElements] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ExamAwardElements_AwardId]
    ON [dbo].[ExamAwardElements]([AwardId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAwardElements_ElementId]
    ON [dbo].[ExamAwardElements]([ElementId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamAwardElements]([ClusterId] ASC);


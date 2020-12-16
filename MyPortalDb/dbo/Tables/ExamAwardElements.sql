CREATE TABLE [dbo].[ExamAwardElements] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AwardId]   UNIQUEIDENTIFIER NOT NULL,
    [ElementId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ExamAwardElements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamAwardElements_ExamAwards_AwardId] FOREIGN KEY ([AwardId]) REFERENCES [dbo].[ExamAwards] ([Id]),
    CONSTRAINT [FK_ExamAwardElements_ExamElements_ElementId] FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ExamElements] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAwardElements_AwardId]
    ON [dbo].[ExamAwardElements]([AwardId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamAwardElements_ElementId]
    ON [dbo].[ExamAwardElements]([ElementId] ASC);


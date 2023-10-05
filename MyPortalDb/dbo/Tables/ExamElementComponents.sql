CREATE TABLE [dbo].[ExamElementComponents] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [ElementId]   UNIQUEIDENTIFIER NOT NULL,
    [ComponentId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ExamElementComponents] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamElementComponents_ExamComponents_ComponentId] FOREIGN KEY ([ComponentId]) REFERENCES [dbo].[ExamComponents] ([Id]),
    CONSTRAINT [FK_ExamElementComponents_ExamElements_ElementId] FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ExamElements] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExamElementComponents]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamElementComponents_ComponentId]
    ON [dbo].[ExamElementComponents]([ComponentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamElementComponents_ElementId]
    ON [dbo].[ExamElementComponents]([ElementId] ASC);


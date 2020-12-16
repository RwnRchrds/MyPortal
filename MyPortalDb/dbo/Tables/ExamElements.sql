CREATE TABLE [dbo].[ExamElements] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BaseElementId] UNIQUEIDENTIFIER NOT NULL,
    [SeriesId]      UNIQUEIDENTIFIER NOT NULL,
    [Description]   NVARCHAR (256)   NULL,
    [ExamFee]       DECIMAL (10, 2)  NULL,
    [Submitted]     BIT              NOT NULL,
    CONSTRAINT [PK_ExamElements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExamElements_ExamBaseElements_BaseElementId] FOREIGN KEY ([BaseElementId]) REFERENCES [dbo].[ExamBaseElements] ([Id]),
    CONSTRAINT [FK_ExamElements_ExamSeries_SeriesId] FOREIGN KEY ([SeriesId]) REFERENCES [dbo].[ExamSeries] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExamElements_BaseElementId]
    ON [dbo].[ExamElements]([BaseElementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExamElements_SeriesId]
    ON [dbo].[ExamElements]([SeriesId] ASC);


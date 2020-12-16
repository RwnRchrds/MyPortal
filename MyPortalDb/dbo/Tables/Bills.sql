CREATE TABLE [dbo].[Bills] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]   UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate] DATETIME2 (7)    NOT NULL,
    [DueDate]     DATETIME2 (7)    NOT NULL,
    [Dispatched]  BIT              NULL,
    CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bills_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Bills_StudentId]
    ON [dbo].[Bills]([StudentId] ASC);


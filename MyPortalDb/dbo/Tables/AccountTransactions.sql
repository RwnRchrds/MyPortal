CREATE TABLE [dbo].[AccountTransactions] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [Amount]    DECIMAL (10, 2)  NOT NULL,
    [Date]      DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_AccountTransactions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountTransactions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AccountTransactions]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AccountTransactions_StudentId]
    ON [dbo].[AccountTransactions]([StudentId] ASC);


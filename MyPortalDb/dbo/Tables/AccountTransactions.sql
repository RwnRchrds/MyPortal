CREATE TABLE [dbo].[AccountTransactions] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [Amount]    DECIMAL (10, 2)  NOT NULL,
    [Credit]    BIT              NOT NULL,
    [Date]      DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_AccountTransactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountTransactions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AccountTransactions_StudentId]
    ON [dbo].[AccountTransactions]([StudentId] ASC);


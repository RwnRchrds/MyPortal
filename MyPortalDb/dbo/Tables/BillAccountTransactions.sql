CREATE TABLE [dbo].[BillAccountTransactions] (
    [Id]                   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BillId]               UNIQUEIDENTIFIER NOT NULL,
    [AccountTransactionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BillAccountTransactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillAccountTransactions_AccountTransactions_AccountTransactionId] FOREIGN KEY ([AccountTransactionId]) REFERENCES [dbo].[AccountTransactions] ([Id]),
    CONSTRAINT [FK_BillAccountTransactions_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillAccountTransactions_AccountTransactionId]
    ON [dbo].[BillAccountTransactions]([AccountTransactionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillAccountTransactions_BillId]
    ON [dbo].[BillAccountTransactions]([BillId] ASC);


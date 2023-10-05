CREATE TABLE [dbo].[BillAccountTransactions] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]            INT              IDENTITY (1, 1) NOT NULL,
    [BillId]               UNIQUEIDENTIFIER NOT NULL,
    [AccountTransactionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BillAccountTransactions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillAccountTransactions_AccountTransactions_AccountTransactionId] FOREIGN KEY ([AccountTransactionId]) REFERENCES [dbo].[AccountTransactions] ([Id]),
    CONSTRAINT [FK_BillAccountTransactions_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[BillAccountTransactions]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillAccountTransactions_AccountTransactionId]
    ON [dbo].[BillAccountTransactions]([AccountTransactionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillAccountTransactions_BillId]
    ON [dbo].[BillAccountTransactions]([BillId] ASC);


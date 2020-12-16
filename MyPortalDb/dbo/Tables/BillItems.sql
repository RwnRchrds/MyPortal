CREATE TABLE [dbo].[BillItems] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BillId]           UNIQUEIDENTIFIER NOT NULL,
    [ProductId]        UNIQUEIDENTIFIER NOT NULL,
    [Quantity]         INT              NOT NULL,
    [GrossAmount]      DECIMAL (18, 2)  NOT NULL,
    [CustomerReceived] BIT              NOT NULL,
    [Refunded]         BIT              NOT NULL,
    CONSTRAINT [PK_BillItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillItems_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id]),
    CONSTRAINT [FK_BillItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillItems_BillId]
    ON [dbo].[BillItems]([BillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillItems_ProductId]
    ON [dbo].[BillItems]([ProductId] ASC);


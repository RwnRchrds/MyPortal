CREATE TABLE [dbo].[BillStoreDiscounts] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BillId]          UNIQUEIDENTIFIER NOT NULL,
    [StoreDiscountId] UNIQUEIDENTIFIER NOT NULL,
    [GrossAmount]     DECIMAL (10, 2)  NOT NULL,
    CONSTRAINT [PK_BillStoreDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillStoreDiscounts_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id]),
    CONSTRAINT [FK_BillStoreDiscounts_StoreDiscounts_StoreDiscountId] FOREIGN KEY ([StoreDiscountId]) REFERENCES [dbo].[StoreDiscounts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillStoreDiscounts_BillId]
    ON [dbo].[BillStoreDiscounts]([BillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillStoreDiscounts_StoreDiscountId]
    ON [dbo].[BillStoreDiscounts]([StoreDiscountId] ASC);


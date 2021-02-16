CREATE TABLE [dbo].[ProductDiscounts] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ProductId]       UNIQUEIDENTIFIER NOT NULL,
    [StoreDiscountId] UNIQUEIDENTIFIER NOT NULL,
    [DiscountId]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_ProductDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id]),
    CONSTRAINT [FK_ProductDiscounts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CONSTRAINT [FK_ProductDiscounts_StoreDiscounts_StoreDiscountId] FOREIGN KEY ([StoreDiscountId]) REFERENCES [dbo].[StoreDiscounts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDiscounts_DiscountId]
    ON [dbo].[ProductDiscounts]([DiscountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDiscounts_ProductId]
    ON [dbo].[ProductDiscounts]([ProductId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDiscounts_StoreDiscountId]
    ON [dbo].[ProductDiscounts]([StoreDiscountId] ASC);


CREATE TABLE [dbo].[StoreDiscounts] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [ProductId]     UNIQUEIDENTIFIER NOT NULL,
    [ProductTypeId] UNIQUEIDENTIFIER NOT NULL,
    [DiscountId]    UNIQUEIDENTIFIER NOT NULL,
    [ApplyToCart]   BIT              NOT NULL,
    [ApplyTo]       INT              NULL,
    CONSTRAINT [PK_StoreDiscounts] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StoreDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StoreDiscounts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CONSTRAINT [FK_StoreDiscounts_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductTypes] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StoreDiscounts]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StoreDiscounts_DiscountId]
    ON [dbo].[StoreDiscounts]([DiscountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StoreDiscounts_ProductId]
    ON [dbo].[StoreDiscounts]([ProductId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StoreDiscounts_ProductTypeId]
    ON [dbo].[StoreDiscounts]([ProductTypeId] ASC);


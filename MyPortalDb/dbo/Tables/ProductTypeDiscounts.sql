CREATE TABLE [dbo].[ProductTypeDiscounts] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StoreDiscountId] UNIQUEIDENTIFIER NOT NULL,
    [ProductTypeId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProductTypeDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductTypeDiscounts_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductTypes] ([Id]),
    CONSTRAINT [FK_ProductTypeDiscounts_StoreDiscounts_StoreDiscountId] FOREIGN KEY ([StoreDiscountId]) REFERENCES [dbo].[StoreDiscounts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductTypeDiscounts_ProductTypeId]
    ON [dbo].[ProductTypeDiscounts]([ProductTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ProductTypeDiscounts_StoreDiscountId]
    ON [dbo].[ProductTypeDiscounts]([StoreDiscountId] ASC);


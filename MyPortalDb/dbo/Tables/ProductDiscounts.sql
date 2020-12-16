CREATE TABLE [dbo].[ProductDiscounts] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ProductId]   UNIQUEIDENTIFIER NOT NULL,
    [DiscountId]  UNIQUEIDENTIFIER NOT NULL,
    [MinRequired] INT              NOT NULL,
    CONSTRAINT [PK_ProductDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id]),
    CONSTRAINT [FK_ProductDiscounts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDiscounts_DiscountId]
    ON [dbo].[ProductDiscounts]([DiscountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDiscounts_ProductId]
    ON [dbo].[ProductDiscounts]([ProductId] ASC);


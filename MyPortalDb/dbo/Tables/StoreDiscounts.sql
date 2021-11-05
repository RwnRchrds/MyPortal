CREATE TABLE [dbo].[StoreDiscounts] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [DiscountId]  UNIQUEIDENTIFIER NOT NULL,
    [MinQuantity] INT              NOT NULL,
    [MaxQuantity] INT              NULL,
    [Auto]        BIT              NOT NULL,
    CONSTRAINT [PK_StoreDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StoreDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_StoreDiscounts_DiscountId]
    ON [dbo].[StoreDiscounts]([DiscountId] ASC);


CREATE TABLE [dbo].[StoreDiscounts] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [DiscountId]  UNIQUEIDENTIFIER NOT NULL,
    [MinQuantity] INT              NOT NULL,
    [MaxQuantity] INT              NULL,
    [Global]      BIT              NOT NULL,
    CONSTRAINT [PK_StoreDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC)
);


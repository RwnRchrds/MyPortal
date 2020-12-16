CREATE TABLE [dbo].[ChargeDiscounts] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ChargeId]   UNIQUEIDENTIFIER NOT NULL,
    [DiscountId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ChargeDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ChargeDiscounts_Charges_ChargeId] FOREIGN KEY ([ChargeId]) REFERENCES [dbo].[Charges] ([Id]),
    CONSTRAINT [FK_ChargeDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ChargeDiscounts_ChargeId]
    ON [dbo].[ChargeDiscounts]([ChargeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ChargeDiscounts_DiscountId]
    ON [dbo].[ChargeDiscounts]([DiscountId] ASC);


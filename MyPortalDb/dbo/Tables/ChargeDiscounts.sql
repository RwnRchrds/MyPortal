﻿CREATE TABLE [dbo].[ChargeDiscounts] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]  INT              IDENTITY (1, 1) NOT NULL,
    [ChargeId]   UNIQUEIDENTIFIER NOT NULL,
    [DiscountId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ChargeDiscounts] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ChargeDiscounts_Charges_ChargeId] FOREIGN KEY ([ChargeId]) REFERENCES [dbo].[Charges] ([Id]),
    CONSTRAINT [FK_ChargeDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ChargeDiscounts]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ChargeDiscounts_ChargeId]
    ON [dbo].[ChargeDiscounts]([ChargeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ChargeDiscounts_DiscountId]
    ON [dbo].[ChargeDiscounts]([DiscountId] ASC);


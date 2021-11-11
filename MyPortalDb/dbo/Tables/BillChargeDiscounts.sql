CREATE TABLE [dbo].[BillChargeDiscounts] (
    [Id]               UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BillId]           UNIQUEIDENTIFIER NOT NULL,
    [ChargeDiscountId] UNIQUEIDENTIFIER NOT NULL,
    [GrossAmount]      DECIMAL (10, 2)  NOT NULL,
    CONSTRAINT [PK_BillChargeDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillChargeDiscounts_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id]),
    CONSTRAINT [FK_BillChargeDiscounts_ChargeDiscounts_ChargeDiscountId] FOREIGN KEY ([ChargeDiscountId]) REFERENCES [dbo].[ChargeDiscounts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillChargeDiscounts_BillId]
    ON [dbo].[BillChargeDiscounts]([BillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillChargeDiscounts_ChargeDiscountId]
    ON [dbo].[BillChargeDiscounts]([ChargeDiscountId] ASC);


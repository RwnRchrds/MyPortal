CREATE TABLE [dbo].[StudentChargeDiscounts] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]        INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]        UNIQUEIDENTIFIER NOT NULL,
    [ChargeDiscountId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_StudentChargeDiscounts] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentChargeDiscounts_ChargeDiscounts_ChargeDiscountId] FOREIGN KEY ([ChargeDiscountId]) REFERENCES [dbo].[ChargeDiscounts] ([Id]),
    CONSTRAINT [FK_StudentChargeDiscounts_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_StudentChargeDiscounts_ChargeDiscountId]
    ON [dbo].[StudentChargeDiscounts]([ChargeDiscountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentChargeDiscounts_StudentId]
    ON [dbo].[StudentChargeDiscounts]([StudentId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StudentChargeDiscounts]([ClusterId] ASC);


CREATE TABLE [dbo].[BillDiscount] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BillId]     UNIQUEIDENTIFIER NOT NULL,
    [DiscountId] UNIQUEIDENTIFIER NOT NULL,
    [Amount]     DECIMAL (10, 2)  NOT NULL,
    [Percentage] BIT              NOT NULL,
    CONSTRAINT [PK_BillDiscount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillDiscount_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id]),
    CONSTRAINT [FK_BillDiscount_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillDiscount_BillId]
    ON [dbo].[BillDiscount]([BillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillDiscount_DiscountId]
    ON [dbo].[BillDiscount]([DiscountId] ASC);


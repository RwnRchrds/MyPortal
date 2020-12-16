CREATE TABLE [dbo].[StudentDiscounts] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]  UNIQUEIDENTIFIER NOT NULL,
    [DiscountId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_StudentDiscounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentDiscounts_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id]),
    CONSTRAINT [FK_StudentDiscounts_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentDiscounts_DiscountId]
    ON [dbo].[StudentDiscounts]([DiscountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentDiscounts_StudentId]
    ON [dbo].[StudentDiscounts]([StudentId] ASC);


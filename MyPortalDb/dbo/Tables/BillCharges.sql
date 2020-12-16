CREATE TABLE [dbo].[BillCharges] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BillId]      UNIQUEIDENTIFIER NOT NULL,
    [ChargeId]    UNIQUEIDENTIFIER NOT NULL,
    [GrossAmount] DECIMAL (10, 2)  NOT NULL,
    [Refunded]    BIT              NOT NULL,
    CONSTRAINT [PK_BillCharges] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillCharges_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id]),
    CONSTRAINT [FK_BillCharges_Charges_ChargeId] FOREIGN KEY ([ChargeId]) REFERENCES [dbo].[Charges] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillCharges_BillId]
    ON [dbo].[BillCharges]([BillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillCharges_ChargeId]
    ON [dbo].[BillCharges]([ChargeId] ASC);


﻿CREATE TABLE [dbo].[BillCharges] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [BillId]          UNIQUEIDENTIFIER NOT NULL,
    [StudentChargeId] UNIQUEIDENTIFIER NOT NULL,
    [NetAmount]       DECIMAL (10, 2)  NOT NULL,
    [VatAmount]       DECIMAL (18, 2)  NOT NULL,
    [Refunded]        BIT              NOT NULL,
    CONSTRAINT [PK_BillCharges] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillCharges_Bills_BillId] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bills] ([Id]),
    CONSTRAINT [FK_BillCharges_StudentCharges_StudentChargeId] FOREIGN KEY ([StudentChargeId]) REFERENCES [dbo].[StudentCharges] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillCharges_BillId]
    ON [dbo].[BillCharges]([BillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BillCharges_StudentChargeId]
    ON [dbo].[BillCharges]([StudentChargeId] ASC);


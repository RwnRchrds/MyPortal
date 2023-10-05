CREATE TABLE [dbo].[StudentCharges] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]             INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]             UNIQUEIDENTIFIER NOT NULL,
    [ChargeId]              UNIQUEIDENTIFIER NOT NULL,
    [ChargeBillingPeriodId] UNIQUEIDENTIFIER NOT NULL,
    [Description]           NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_StudentCharges] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentCharges_ChargeBillingPeriods_ChargeBillingPeriodId] FOREIGN KEY ([ChargeBillingPeriodId]) REFERENCES [dbo].[ChargeBillingPeriods] ([Id]),
    CONSTRAINT [FK_StudentCharges_Charges_ChargeId] FOREIGN KEY ([ChargeId]) REFERENCES [dbo].[Charges] ([Id]),
    CONSTRAINT [FK_StudentCharges_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StudentCharges]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentCharges_ChargeBillingPeriodId]
    ON [dbo].[StudentCharges]([ChargeBillingPeriodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentCharges_ChargeId]
    ON [dbo].[StudentCharges]([ChargeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentCharges_StudentId]
    ON [dbo].[StudentCharges]([StudentId] ASC);


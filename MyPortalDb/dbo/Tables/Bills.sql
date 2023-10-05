CREATE TABLE [dbo].[Bills] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]             INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]             UNIQUEIDENTIFIER NOT NULL,
    [ChargeBillingPeriodId] UNIQUEIDENTIFIER NULL,
    [CreatedDate]           DATETIME2 (7)    NOT NULL,
    [DueDate]               DATETIME2 (7)    NOT NULL,
    [Dispatched]            BIT              NULL,
    CONSTRAINT [PK_Bills] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bills_ChargeBillingPeriods_ChargeBillingPeriodId] FOREIGN KEY ([ChargeBillingPeriodId]) REFERENCES [dbo].[ChargeBillingPeriods] ([Id]),
    CONSTRAINT [FK_Bills_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Bills]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Bills_ChargeBillingPeriodId]
    ON [dbo].[Bills]([ChargeBillingPeriodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Bills_StudentId]
    ON [dbo].[Bills]([StudentId] ASC);


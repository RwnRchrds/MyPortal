CREATE TABLE [dbo].[Charges] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [VatRateId]   UNIQUEIDENTIFIER NOT NULL,
    [Code]        NVARCHAR (64)    NULL,
    [Name]        NVARCHAR (128)   NULL,
    [Amount]      DECIMAL (10, 2)  NOT NULL,
    [Variable]    BIT              NOT NULL,
    CONSTRAINT [PK_Charges] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Charges_VatRates_VatRateId] FOREIGN KEY ([VatRateId]) REFERENCES [dbo].[VatRates] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Charges]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Charges_VatRateId]
    ON [dbo].[Charges]([VatRateId] ASC);


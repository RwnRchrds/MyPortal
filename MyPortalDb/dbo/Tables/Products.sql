CREATE TABLE [dbo].[Products] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ProductTypeId] UNIQUEIDENTIFIER NOT NULL,
    [VatRateId]     UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (128)   NOT NULL,
    [Description]   NVARCHAR (256)   NOT NULL,
    [Price]         DECIMAL (10, 2)  NOT NULL,
    [ShowOnStore]   BIT              NOT NULL,
    [OrderLimit]    INT              NOT NULL,
    [Deleted]       BIT              NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Products_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductTypes] ([Id]),
    CONSTRAINT [FK_Products_VatRates_VatRateId] FOREIGN KEY ([VatRateId]) REFERENCES [dbo].[VatRates] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Products_ProductTypeId]
    ON [dbo].[Products]([ProductTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Products_VatRateId]
    ON [dbo].[Products]([VatRateId] ASC);


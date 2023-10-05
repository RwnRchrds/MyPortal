CREATE TABLE [dbo].[AddressAgencies] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [AddressId]     UNIQUEIDENTIFIER NOT NULL,
    [AgencyId]      UNIQUEIDENTIFIER NOT NULL,
    [AddressTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Main]          BIT              NOT NULL,
    CONSTRAINT [PK_AddressAgencies] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AddressAgencies_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AddressAgencies_AddressTypes_AddressTypeId] FOREIGN KEY ([AddressTypeId]) REFERENCES [dbo].[AddressTypes] ([Id]),
    CONSTRAINT [FK_AddressAgencies_Agencies_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agencies] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AddressAgencies]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AddressAgencies_AddressId]
    ON [dbo].[AddressAgencies]([AddressId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AddressAgencies_AddressTypeId]
    ON [dbo].[AddressAgencies]([AddressTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AddressAgencies_AgencyId]
    ON [dbo].[AddressAgencies]([AgencyId] ASC);


﻿CREATE TABLE [dbo].[AddressPeople] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [AddressId]     UNIQUEIDENTIFIER NOT NULL,
    [PersonId]      UNIQUEIDENTIFIER NULL,
    [AddressTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Main]          BIT              NOT NULL,
    CONSTRAINT [PK_AddressPeople] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AddressPeople_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AddressPeople_AddressTypes_AddressTypeId] FOREIGN KEY ([AddressTypeId]) REFERENCES [dbo].[AddressTypes] ([Id]),
    CONSTRAINT [FK_AddressPeople_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AddressPeople]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AddressPeople_AddressId]
    ON [dbo].[AddressPeople]([AddressId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AddressPeople_AddressTypeId]
    ON [dbo].[AddressPeople]([AddressTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AddressPeople_PersonId]
    ON [dbo].[AddressPeople]([PersonId] ASC);


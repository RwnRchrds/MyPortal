CREATE TABLE [dbo].[AddressPeople] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AddressId] UNIQUEIDENTIFIER NOT NULL,
    [PersonId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_AddressPeople] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AddressPeople_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([Id]),
    CONSTRAINT [FK_AddressPeople_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AddressPeople_AddressId]
    ON [dbo].[AddressPeople]([AddressId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AddressPeople_PersonId]
    ON [dbo].[AddressPeople]([PersonId] ASC);


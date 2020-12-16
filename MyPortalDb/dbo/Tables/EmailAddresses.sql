CREATE TABLE [dbo].[EmailAddresses] (
    [Id]       UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [TypeId]   UNIQUEIDENTIFIER NOT NULL,
    [PersonId] UNIQUEIDENTIFIER NOT NULL,
    [AgencyId] UNIQUEIDENTIFIER NULL,
    [Address]  NVARCHAR (128)   NOT NULL,
    [Main]     BIT              NOT NULL,
    [Notes]    NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_EmailAddresses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmailAddresses_EmailAddressTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[EmailAddressTypes] ([Id]),
    CONSTRAINT [FK_EmailAddresses_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EmailAddresses_PersonId]
    ON [dbo].[EmailAddresses]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EmailAddresses_TypeId]
    ON [dbo].[EmailAddresses]([TypeId] ASC);


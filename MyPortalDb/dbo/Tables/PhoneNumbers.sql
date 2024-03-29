﻿CREATE TABLE [dbo].[PhoneNumbers] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [TypeId]    UNIQUEIDENTIFIER NOT NULL,
    [PersonId]  UNIQUEIDENTIFIER NULL,
    [AgencyId]  UNIQUEIDENTIFIER NULL,
    [Number]    NVARCHAR (128)   NULL,
    [Main]      BIT              NOT NULL,
    CONSTRAINT [PK_PhoneNumbers] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhoneNumbers_Agencies_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agencies] ([Id]),
    CONSTRAINT [FK_PhoneNumbers_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_PhoneNumbers_PhoneNumberTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[PhoneNumberTypes] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[PhoneNumbers]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneNumbers_AgencyId]
    ON [dbo].[PhoneNumbers]([AgencyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneNumbers_PersonId]
    ON [dbo].[PhoneNumbers]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneNumbers_TypeId]
    ON [dbo].[PhoneNumbers]([TypeId] ASC);


CREATE TABLE [dbo].[PhoneNumbers] (
    [Id]       UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [TypeId]   UNIQUEIDENTIFIER NOT NULL,
    [PersonId] UNIQUEIDENTIFIER NOT NULL,
    [AgencyId] UNIQUEIDENTIFIER NULL,
    [Number]   NVARCHAR (128)   NULL,
    [Main]     BIT              NOT NULL,
    CONSTRAINT [PK_PhoneNumbers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhoneNumbers_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_PhoneNumbers_PhoneNumberTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[PhoneNumberTypes] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneNumbers_PersonId]
    ON [dbo].[PhoneNumbers]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneNumbers_TypeId]
    ON [dbo].[PhoneNumbers]([TypeId] ASC);


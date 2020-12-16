CREATE TABLE [dbo].[Contacts] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [PersonId]       UNIQUEIDENTIFIER NOT NULL,
    [ParentalBallot] BIT              NOT NULL,
    [PlaceOfWork]    NVARCHAR (256)   NULL,
    [JobTitle]       NVARCHAR (256)   NULL,
    [NiNumber]       NVARCHAR (128)   NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contacts_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Contacts_PersonId]
    ON [dbo].[Contacts]([PersonId] ASC);


CREATE TABLE [dbo].[Contacts] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [PersonId]       UNIQUEIDENTIFIER NOT NULL,
    [ParentalBallot] BIT              NOT NULL,
    [PlaceOfWork]    NVARCHAR (256)   NULL,
    [JobTitle]       NVARCHAR (256)   NULL,
    [NiNumber]       NVARCHAR (128)   NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contacts_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Contacts]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contacts_PersonId]
    ON [dbo].[Contacts]([PersonId] ASC);


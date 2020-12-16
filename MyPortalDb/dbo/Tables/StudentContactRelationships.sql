CREATE TABLE [dbo].[StudentContactRelationships] (
    [Id]                     UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [RelationshipTypeId]     UNIQUEIDENTIFIER NOT NULL,
    [StudentId]              UNIQUEIDENTIFIER NOT NULL,
    [ContactId]              UNIQUEIDENTIFIER NOT NULL,
    [Correspondence]         BIT              NOT NULL,
    [ParentalResponsibility] BIT              NOT NULL,
    [PupilReport]            BIT              NOT NULL,
    [CourtOrder]             BIT              NOT NULL,
    CONSTRAINT [PK_StudentContactRelationships] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentContactRelationships_ContactRelationshipTypes_RelationshipTypeId] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [dbo].[ContactRelationshipTypes] ([Id]),
    CONSTRAINT [FK_StudentContactRelationships_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([Id]),
    CONSTRAINT [FK_StudentContactRelationships_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentContactRelationships_ContactId]
    ON [dbo].[StudentContactRelationships]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentContactRelationships_RelationshipTypeId]
    ON [dbo].[StudentContactRelationships]([RelationshipTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentContactRelationships_StudentId]
    ON [dbo].[StudentContactRelationships]([StudentId] ASC);


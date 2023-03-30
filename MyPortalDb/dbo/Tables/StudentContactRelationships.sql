CREATE TABLE [dbo].[StudentContactRelationships] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]              INT              IDENTITY (1, 1) NOT NULL,
    [RelationshipTypeId]     UNIQUEIDENTIFIER NOT NULL,
    [StudentId]              UNIQUEIDENTIFIER NOT NULL,
    [ContactId]              UNIQUEIDENTIFIER NOT NULL,
    [Correspondence]         BIT              NOT NULL,
    [ParentalResponsibility] BIT              NOT NULL,
    [PupilReport]            BIT              NOT NULL,
    [CourtOrder]             BIT              NOT NULL,
    CONSTRAINT [PK_StudentContactRelationships] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentContactRelationships_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([Id]),
    CONSTRAINT [FK_StudentContactRelationships_RelationshipTypes_RelationshipTypeId] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [dbo].[RelationshipTypes] ([Id]),
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


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StudentContactRelationships]([ClusterId] ASC);


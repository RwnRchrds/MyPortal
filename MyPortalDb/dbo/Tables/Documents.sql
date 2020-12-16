CREATE TABLE [dbo].[Documents] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [TypeId]      UNIQUEIDENTIFIER NOT NULL,
    [DirectoryId] UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (128)   NOT NULL,
    [Description] NVARCHAR (256)   NULL,
    [CreatedById] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate] DATE             NOT NULL,
    [Restricted]  BIT              NOT NULL,
    [Deleted]     BIT              NOT NULL,
    CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Documents_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]),
    CONSTRAINT [FK_Documents_DocumentTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[DocumentTypes] ([Id]),
    CONSTRAINT [FK_Documents_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_CreatedById]
    ON [dbo].[Documents]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_DirectoryId]
    ON [dbo].[Documents]([DirectoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_TypeId]
    ON [dbo].[Documents]([TypeId] ASC);


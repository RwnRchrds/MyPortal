CREATE TABLE [dbo].[Documents] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [TypeId]      UNIQUEIDENTIFIER NOT NULL,
    [DirectoryId] UNIQUEIDENTIFIER NOT NULL,
    [FileId]      UNIQUEIDENTIFIER NULL,
    [Title]       NVARCHAR (128)   NOT NULL,
    [Description] NVARCHAR (256)   NULL,
    [CreatedById] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate] DATETIME2 (7)    NOT NULL,
    [Private]     BIT              NOT NULL,
    [Deleted]     BIT              NOT NULL,
    CONSTRAINT [PK_Documents] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Documents_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]),
    CONSTRAINT [FK_Documents_DocumentTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[DocumentTypes] ([Id]),
    CONSTRAINT [FK_Documents_Files_FileId] FOREIGN KEY ([FileId]) REFERENCES [dbo].[Files] ([Id]),
    CONSTRAINT [FK_Documents_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Documents]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_CreatedById]
    ON [dbo].[Documents]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_DirectoryId]
    ON [dbo].[Documents]([DirectoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_FileId]
    ON [dbo].[Documents]([FileId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_TypeId]
    ON [dbo].[Documents]([TypeId] ASC);


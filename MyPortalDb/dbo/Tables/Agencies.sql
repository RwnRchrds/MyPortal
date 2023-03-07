CREATE TABLE [dbo].[Agencies] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [TypeId]      UNIQUEIDENTIFIER NOT NULL,
    [DirectoryId] UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (256)   NULL,
    [Website]     NVARCHAR (100)   NULL,
    [Deleted]     BIT              NOT NULL,
    CONSTRAINT [PK_Agencies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Agencies_AgencyTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[AgencyTypes] ([Id]),
    CONSTRAINT [FK_Agencies_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Agencies_DirectoryId]
    ON [dbo].[Agencies]([DirectoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Agencies_TypeId]
    ON [dbo].[Agencies]([TypeId] ASC);


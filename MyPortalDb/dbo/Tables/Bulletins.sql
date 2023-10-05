CREATE TABLE [dbo].[Bulletins] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [DirectoryId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedById] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate] DATETIME2 (7)    NOT NULL,
    [ExpireDate]  DATETIME2 (7)    NULL,
    [Title]       NVARCHAR (50)    NOT NULL,
    [Detail]      NVARCHAR (MAX)   NOT NULL,
    [Private]     BIT              NOT NULL,
    [Approved]    BIT              NOT NULL,
    CONSTRAINT [PK_Bulletins] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bulletins_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]),
    CONSTRAINT [FK_Bulletins_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Bulletins]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Bulletins_CreatedById]
    ON [dbo].[Bulletins]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Bulletins_DirectoryId]
    ON [dbo].[Bulletins]([DirectoryId] ASC);


CREATE TABLE [dbo].[Bulletins] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [DirectoryId] UNIQUEIDENTIFIER NOT NULL,
    [AuthorId]    UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]  DATETIME2 (7)    NOT NULL,
    [ExpireDate]  DATETIME2 (7)    NULL,
    [Title]       NVARCHAR (128)   NOT NULL,
    [Detail]      NVARCHAR (MAX)   NOT NULL,
    [StaffOnly]   BIT              NOT NULL,
    [Approved]    BIT              NOT NULL,
    CONSTRAINT [PK_Bulletins] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bulletins_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id]),
    CONSTRAINT [FK_Bulletins_Users_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Bulletins_AuthorId]
    ON [dbo].[Bulletins]([AuthorId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Bulletins_DirectoryId]
    ON [dbo].[Bulletins]([DirectoryId] ASC);


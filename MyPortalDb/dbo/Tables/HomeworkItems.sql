CREATE TABLE [dbo].[HomeworkItems] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [DirectoryId]  UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (128)   NOT NULL,
    [Description]  NVARCHAR (256)   NULL,
    [SubmitOnline] BIT              NOT NULL,
    [MaxPoints]    INT              NOT NULL,
    CONSTRAINT [PK_HomeworkItems] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HomeworkItems_Directories_DirectoryId] FOREIGN KEY ([DirectoryId]) REFERENCES [dbo].[Directories] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[HomeworkItems]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HomeworkItems_DirectoryId]
    ON [dbo].[HomeworkItems]([DirectoryId] ASC);


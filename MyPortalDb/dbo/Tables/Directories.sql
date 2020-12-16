CREATE TABLE [dbo].[Directories] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ParentId]  UNIQUEIDENTIFIER NULL,
    [Name]      NVARCHAR (128)   NOT NULL,
    [Private]   BIT              NOT NULL,
    [StaffOnly] BIT              NOT NULL,
    CONSTRAINT [PK_Directories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Directories_Directories_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Directories] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Directories_ParentId]
    ON [dbo].[Directories]([ParentId] ASC);


CREATE TABLE [dbo].[Directories] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [ParentId]  UNIQUEIDENTIFIER NULL,
    [Name]      NVARCHAR (128)   NOT NULL,
    [Private]   BIT              NOT NULL,
    CONSTRAINT [PK_Directories] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Directories_Directories_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Directories] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Directories_ParentId]
    ON [dbo].[Directories]([ParentId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Directories]([ClusterId] ASC);


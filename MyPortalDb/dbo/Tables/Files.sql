CREATE TABLE [dbo].[Files] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [DocumentId]  UNIQUEIDENTIFIER NOT NULL,
    [FileId]      NVARCHAR (MAX)   NOT NULL,
    [FileName]    NVARCHAR (MAX)   NOT NULL,
    [ContentType] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Files_Documents_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Documents] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Files_DocumentId]
    ON [dbo].[Files]([DocumentId] ASC);


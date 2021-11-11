CREATE TABLE [dbo].[Files] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [FileId]      NVARCHAR (MAX)   NOT NULL,
    [FileName]    NVARCHAR (MAX)   NOT NULL,
    [ContentType] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED ([Id] ASC)
);


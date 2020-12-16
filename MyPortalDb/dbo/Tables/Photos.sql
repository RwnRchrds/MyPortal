CREATE TABLE [dbo].[Photos] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Data]      VARBINARY (MAX)  NULL,
    [PhotoDate] DATETIME2 (7)    NOT NULL,
    [MimeType]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED ([Id] ASC)
);


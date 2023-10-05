CREATE TABLE [dbo].[Photos] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [Data]      VARBINARY (MAX)  NULL,
    [PhotoDate] DATE             NOT NULL,
    [MimeType]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Photos] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Photos]([ClusterId] ASC);


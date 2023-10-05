CREATE TABLE [dbo].[Files] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [FileId]      NVARCHAR (MAX)   NOT NULL,
    [FileName]    NVARCHAR (MAX)   NOT NULL,
    [ContentType] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Files]([ClusterId] ASC);


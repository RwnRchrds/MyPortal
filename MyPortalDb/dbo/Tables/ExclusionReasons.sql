CREATE TABLE [dbo].[ExclusionReasons] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Code]        NVARCHAR (MAX)   NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_ExclusionReasons] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ExclusionReasons]([ClusterId] ASC);


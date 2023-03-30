CREATE TABLE [dbo].[Locations] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (128)   NOT NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Locations]([ClusterId] ASC);


CREATE TABLE [dbo].[TaskTypes] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Personal]    BIT              NOT NULL,
    [ColourCode]  NVARCHAR (MAX)   NOT NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_TaskTypes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[TaskTypes]([ClusterId] ASC);


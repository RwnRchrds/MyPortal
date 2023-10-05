CREATE TABLE [dbo].[DiaryEventTypes] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [ColourCode]  NVARCHAR (128)   NULL,
    [System]      BIT              NOT NULL,
    CONSTRAINT [PK_DiaryEventTypes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[DiaryEventTypes]([ClusterId] ASC);


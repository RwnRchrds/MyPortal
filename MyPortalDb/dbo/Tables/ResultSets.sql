CREATE TABLE [dbo].[ResultSets] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Name]        NVARCHAR (256)   NOT NULL,
    [PublishDate] DATE             NULL,
    [Locked]      BIT              NOT NULL,
    CONSTRAINT [PK_ResultSets] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ResultSets]([ClusterId] ASC);


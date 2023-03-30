CREATE TABLE [dbo].[DetentionTypes] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [StartTime]   TIME (2)         NOT NULL,
    [EndTime]     TIME (2)         NOT NULL,
    CONSTRAINT [PK_DetentionTypes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[DetentionTypes]([ClusterId] ASC);


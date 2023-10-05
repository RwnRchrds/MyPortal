CREATE TABLE [dbo].[DiaryEventTemplates] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [EventTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Minutes]     INT              NOT NULL,
    [Hours]       INT              NOT NULL,
    [Days]        INT              NOT NULL,
    CONSTRAINT [PK_DiaryEventTemplates] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiaryEventTemplates_DiaryEventTypes_EventTypeId] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[DiaryEventTypes] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[DiaryEventTemplates]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DiaryEventTemplates_EventTypeId]
    ON [dbo].[DiaryEventTemplates]([EventTypeId] ASC);


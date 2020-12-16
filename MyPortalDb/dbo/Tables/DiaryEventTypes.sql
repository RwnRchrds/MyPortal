CREATE TABLE [dbo].[DiaryEventTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [ColourCode]  NVARCHAR (128)   NULL,
    [System]      BIT              NOT NULL,
    [Reserved]    BIT              NOT NULL,
    CONSTRAINT [PK_DiaryEventTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


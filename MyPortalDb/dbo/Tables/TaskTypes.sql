CREATE TABLE [dbo].[TaskTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Personal]    BIT              NOT NULL,
    [ColourCode]  NVARCHAR (MAX)   NOT NULL,
    [System]      BIT              NOT NULL,
    [Reserved]    BIT              NOT NULL,
    CONSTRAINT [PK_TaskTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[RoomClosureReasons] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [System]      BIT              NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_RoomClosureReasons] PRIMARY KEY CLUSTERED ([Id] ASC)
);


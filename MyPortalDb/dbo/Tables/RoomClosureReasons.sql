CREATE TABLE [dbo].[RoomClosureReasons] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [System]      BIT              NOT NULL,
    [Exam]        BIT              NOT NULL,
    CONSTRAINT [PK_RoomClosureReasons] PRIMARY KEY CLUSTERED ([Id] ASC)
);


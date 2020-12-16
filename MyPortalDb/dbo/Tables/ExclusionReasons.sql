CREATE TABLE [dbo].[ExclusionReasons] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_ExclusionReasons] PRIMARY KEY CLUSTERED ([Id] ASC)
);


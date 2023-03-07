CREATE TABLE [dbo].[CommentTypes] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_CommentTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


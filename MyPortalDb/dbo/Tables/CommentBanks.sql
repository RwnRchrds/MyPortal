CREATE TABLE [dbo].[CommentBanks] (
    [Id]     UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]   NVARCHAR (128)   NOT NULL,
    [Active] BIT              NOT NULL,
    CONSTRAINT [PK_CommentBanks] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Comments] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [CommentBankId] UNIQUEIDENTIFIER NOT NULL,
    [Value]         NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comments_CommentBanks_CommentBankId] FOREIGN KEY ([CommentBankId]) REFERENCES [dbo].[CommentBanks] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Comments_CommentBankId]
    ON [dbo].[Comments]([CommentBankId] ASC);


CREATE TABLE [dbo].[UserRefreshTokens] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [Value]          NVARCHAR (MAX)   NULL,
    [ExpirationDate] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_UserRefreshTokens] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRefreshTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserRefreshTokens_UserId]
    ON [dbo].[UserRefreshTokens]([UserId] ASC);


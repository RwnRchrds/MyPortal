CREATE TABLE [dbo].[UserLogins] (
    [LoginProvider]       NVARCHAR (450)   NOT NULL,
    [ProviderKey]         NVARCHAR (450)   NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX)   NULL,
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId]
    ON [dbo].[UserLogins]([UserId] ASC);


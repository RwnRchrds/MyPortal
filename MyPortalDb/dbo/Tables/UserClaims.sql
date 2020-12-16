CREATE TABLE [dbo].[UserClaims] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]  NVARCHAR (MAX)   NULL,
    [ClaimValue] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId]
    ON [dbo].[UserClaims]([UserId] ASC);


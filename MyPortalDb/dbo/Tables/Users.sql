CREATE TABLE [dbo].[Users] (
    [Id]                   UNIQUEIDENTIFIER   NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [CreatedDate]          DATETIME2 (7)      NOT NULL,
    [PersonId]             UNIQUEIDENTIFIER   NULL,
    [UserType]             INT                NOT NULL,
    [Enabled]              BIT                NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[Users]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[Users]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_PersonId]
    ON [dbo].[Users]([PersonId] ASC) WHERE ([PersonId] IS NOT NULL);


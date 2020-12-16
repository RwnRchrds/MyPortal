CREATE TABLE [dbo].[RolePermissions] (
    [RoleId]       UNIQUEIDENTIFIER NOT NULL,
    [PermissionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED ([RoleId] ASC, [PermissionId] ASC),
    CONSTRAINT [FK_RolePermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ([Id]),
    CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_RolePermissions_PermissionId]
    ON [dbo].[RolePermissions]([PermissionId] ASC);


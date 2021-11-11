CREATE TABLE [dbo].[SubjectStaffMemberRoles] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [System]      BIT              NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_SubjectStaffMemberRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


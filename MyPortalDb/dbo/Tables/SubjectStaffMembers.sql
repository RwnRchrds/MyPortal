﻿CREATE TABLE [dbo].[SubjectStaffMembers] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [SubjectId]     UNIQUEIDENTIFIER NOT NULL,
    [StaffMemberId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SubjectStaffMembers] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubjectStaffMembers_StaffMembers_StaffMemberId] FOREIGN KEY ([StaffMemberId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_SubjectStaffMembers_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id]),
    CONSTRAINT [FK_SubjectStaffMembers_SubjectStaffMemberRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[SubjectStaffMemberRoles] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[SubjectStaffMembers]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SubjectStaffMembers_RoleId]
    ON [dbo].[SubjectStaffMembers]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SubjectStaffMembers_StaffMemberId]
    ON [dbo].[SubjectStaffMembers]([StaffMemberId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SubjectStaffMembers_SubjectId]
    ON [dbo].[SubjectStaffMembers]([SubjectId] ASC);


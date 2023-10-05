CREATE TABLE [dbo].[SubjectStaffMemberRoles] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [Description]   NVARCHAR (256)   NOT NULL,
    [Active]        BIT              NOT NULL,
    [SubjectLeader] BIT              NOT NULL,
    CONSTRAINT [PK_SubjectStaffMemberRoles] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[SubjectStaffMemberRoles]([ClusterId] ASC);


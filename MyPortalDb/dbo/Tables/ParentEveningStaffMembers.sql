CREATE TABLE [dbo].[ParentEveningStaffMembers] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]         INT              IDENTITY (1, 1) NOT NULL,
    [ParentEveningId]   UNIQUEIDENTIFIER NOT NULL,
    [StaffMemberId]     UNIQUEIDENTIFIER NOT NULL,
    [AvailableFrom]     DATETIME2 (7)    NULL,
    [AvailableTo]       DATETIME2 (7)    NULL,
    [AppointmentLength] INT              NOT NULL,
    [BreakLimit]        INT              NOT NULL,
    CONSTRAINT [PK_ParentEveningStaffMembers] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParentEveningStaffMembers_ParentEvenings_ParentEveningId] FOREIGN KEY ([ParentEveningId]) REFERENCES [dbo].[ParentEvenings] ([Id]),
    CONSTRAINT [FK_ParentEveningStaffMembers_StaffMembers_StaffMemberId] FOREIGN KEY ([StaffMemberId]) REFERENCES [dbo].[StaffMembers] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ParentEveningStaffMembers_ParentEveningId]
    ON [dbo].[ParentEveningStaffMembers]([ParentEveningId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ParentEveningStaffMembers_StaffMemberId]
    ON [dbo].[ParentEveningStaffMembers]([StaffMemberId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ParentEveningStaffMembers]([ClusterId] ASC);


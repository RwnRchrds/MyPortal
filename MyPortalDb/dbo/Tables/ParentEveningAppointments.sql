CREATE TABLE [dbo].[ParentEveningAppointments] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]            INT              IDENTITY (1, 1) NOT NULL,
    [ParentEveningStaffId] UNIQUEIDENTIFIER NOT NULL,
    [StudentId]            UNIQUEIDENTIFIER NOT NULL,
    [Start]                DATETIME2 (7)    NOT NULL,
    [End]                  DATETIME2 (7)    NOT NULL,
    [TeacherAccepted]      BIT              NOT NULL,
    [ParentAccepted]       BIT              NOT NULL,
    [Attended]             BIT              NULL,
    CONSTRAINT [PK_ParentEveningAppointments] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParentEveningAppointments_ParentEveningStaffMembers_ParentEveningStaffId] FOREIGN KEY ([ParentEveningStaffId]) REFERENCES [dbo].[ParentEveningStaffMembers] ([Id]),
    CONSTRAINT [FK_ParentEveningAppointments_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ParentEveningAppointments_ParentEveningStaffId]
    ON [dbo].[ParentEveningAppointments]([ParentEveningStaffId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ParentEveningAppointments_StudentId]
    ON [dbo].[ParentEveningAppointments]([StudentId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ParentEveningAppointments]([ClusterId] ASC);


CREATE TABLE [dbo].[StaffAbsences] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [StaffMemberId] UNIQUEIDENTIFIER NOT NULL,
    [AbsenceTypeId] UNIQUEIDENTIFIER NOT NULL,
    [IllnessTypeId] UNIQUEIDENTIFIER NULL,
    [StartDate]     DATETIME2 (7)    NOT NULL,
    [EndDate]       DATETIME2 (7)    NOT NULL,
    [Confidential]  BIT              NOT NULL,
    [Notes]         NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_StaffAbsences] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StaffAbsences_StaffAbsenceTypes_AbsenceTypeId] FOREIGN KEY ([AbsenceTypeId]) REFERENCES [dbo].[StaffAbsenceTypes] ([Id]),
    CONSTRAINT [FK_StaffAbsences_StaffIllnessTypes_IllnessTypeId] FOREIGN KEY ([IllnessTypeId]) REFERENCES [dbo].[StaffIllnessTypes] ([Id]),
    CONSTRAINT [FK_StaffAbsences_StaffMembers_StaffMemberId] FOREIGN KEY ([StaffMemberId]) REFERENCES [dbo].[StaffMembers] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_StaffAbsences_AbsenceTypeId]
    ON [dbo].[StaffAbsences]([AbsenceTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StaffAbsences_IllnessTypeId]
    ON [dbo].[StaffAbsences]([IllnessTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StaffAbsences_StaffMemberId]
    ON [dbo].[StaffAbsences]([StaffMemberId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StaffAbsences]([ClusterId] ASC);


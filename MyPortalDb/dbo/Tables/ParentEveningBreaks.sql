CREATE TABLE [dbo].[ParentEveningBreaks] (
    [Id]                         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ParentEveningStaffMemberId] UNIQUEIDENTIFIER NOT NULL,
    [Start]                      DATETIME2 (7)    NOT NULL,
    [End]                        DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ParentEveningBreaks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParentEveningBreaks_ParentEveningStaffMembers_ParentEveningStaffMemberId] FOREIGN KEY ([ParentEveningStaffMemberId]) REFERENCES [dbo].[ParentEveningStaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ParentEveningBreaks_ParentEveningStaffMemberId]
    ON [dbo].[ParentEveningBreaks]([ParentEveningStaffMemberId] ASC);


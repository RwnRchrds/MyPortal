CREATE TABLE [dbo].[ParentEveningBreaks] (
    [Id]                         UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]                  INT              IDENTITY (1, 1) NOT NULL,
    [ParentEveningStaffMemberId] UNIQUEIDENTIFIER NOT NULL,
    [Start]                      DATETIME2 (7)    NOT NULL,
    [End]                        DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ParentEveningBreaks] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParentEveningBreaks_ParentEveningStaffMembers_ParentEveningStaffMemberId] FOREIGN KEY ([ParentEveningStaffMemberId]) REFERENCES [dbo].[ParentEveningStaffMembers] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[ParentEveningBreaks]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ParentEveningBreaks_ParentEveningStaffMemberId]
    ON [dbo].[ParentEveningBreaks]([ParentEveningStaffMemberId] ASC);


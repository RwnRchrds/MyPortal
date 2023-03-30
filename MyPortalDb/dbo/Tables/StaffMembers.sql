CREATE TABLE [dbo].[StaffMembers] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [LineManagerId]  UNIQUEIDENTIFIER NULL,
    [PersonId]       UNIQUEIDENTIFIER NOT NULL,
    [Code]           NVARCHAR (128)   NOT NULL,
    [BankName]       NVARCHAR (50)    NULL,
    [BankAccount]    NVARCHAR (15)    NULL,
    [BankSortCode]   NVARCHAR (10)    NULL,
    [NiNumber]       NVARCHAR (9)     NULL,
    [Qualifications] NVARCHAR (128)   NULL,
    [TeachingStaff]  BIT              NOT NULL,
    [Deleted]        BIT              NOT NULL,
    CONSTRAINT [PK_StaffMembers] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StaffMembers_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_StaffMembers_StaffMembers_LineManagerId] FOREIGN KEY ([LineManagerId]) REFERENCES [dbo].[StaffMembers] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_StaffMembers_LineManagerId]
    ON [dbo].[StaffMembers]([LineManagerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StaffMembers_PersonId]
    ON [dbo].[StaffMembers]([PersonId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[StaffMembers]([ClusterId] ASC);


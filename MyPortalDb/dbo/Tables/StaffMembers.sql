CREATE TABLE [dbo].[StaffMembers] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [LineManagerId] UNIQUEIDENTIFIER NULL,
    [PersonId]      UNIQUEIDENTIFIER NOT NULL,
    [Code]          NVARCHAR (128)   NOT NULL,
    [NiNumber]      NVARCHAR (128)   NULL,
    [PostNominal]   NVARCHAR (128)   NULL,
    [TeachingStaff] BIT              NOT NULL,
    [Deleted]       BIT              NOT NULL,
    CONSTRAINT [PK_StaffMembers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StaffMembers_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_StaffMembers_StaffMembers_LineManagerId] FOREIGN KEY ([LineManagerId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StaffMembers_LineManagerId]
    ON [dbo].[StaffMembers]([LineManagerId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_StaffMembers_PersonId]
    ON [dbo].[StaffMembers]([PersonId] ASC);


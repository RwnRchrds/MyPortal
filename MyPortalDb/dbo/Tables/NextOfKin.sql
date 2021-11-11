CREATE TABLE [dbo].[NextOfKin] (
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StaffMemberId]      UNIQUEIDENTIFIER NOT NULL,
    [NextOfKinPersonId]  UNIQUEIDENTIFIER NOT NULL,
    [RelationshipTypeId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_NextOfKin] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NextOfKin_NextOfKinRelationshipTypes_RelationshipTypeId] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [dbo].[NextOfKinRelationshipTypes] ([Id]),
    CONSTRAINT [FK_NextOfKin_People_NextOfKinPersonId] FOREIGN KEY ([NextOfKinPersonId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_NextOfKin_StaffMembers_StaffMemberId] FOREIGN KEY ([StaffMemberId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_NextOfKin_NextOfKinPersonId]
    ON [dbo].[NextOfKin]([NextOfKinPersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NextOfKin_RelationshipTypeId]
    ON [dbo].[NextOfKin]([RelationshipTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NextOfKin_StaffMemberId]
    ON [dbo].[NextOfKin]([StaffMemberId] ASC);


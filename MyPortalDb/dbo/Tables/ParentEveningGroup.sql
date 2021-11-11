﻿CREATE TABLE [dbo].[ParentEveningGroup] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ParentEveningId] UNIQUEIDENTIFIER NOT NULL,
    [StudentGroupId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ParentEveningGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParentEveningGroup_ParentEvenings_ParentEveningId] FOREIGN KEY ([ParentEveningId]) REFERENCES [dbo].[ParentEvenings] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ParentEveningGroup_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ParentEveningGroup_ParentEveningId]
    ON [dbo].[ParentEveningGroup]([ParentEveningId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ParentEveningGroup_StudentGroupId]
    ON [dbo].[ParentEveningGroup]([StudentGroupId] ASC);


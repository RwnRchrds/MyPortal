﻿CREATE TABLE [dbo].[StudentGroupMemberships] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]      UNIQUEIDENTIFIER NOT NULL,
    [StudentGroupId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate]      DATE             NOT NULL,
    [EndDate]        DATE             NULL,
    CONSTRAINT [PK_StudentGroupMemberships] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentGroupMemberships_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id]),
    CONSTRAINT [FK_StudentGroupMemberships_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentGroupMemberships_StudentGroupId]
    ON [dbo].[StudentGroupMemberships]([StudentGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentGroupMemberships_StudentId]
    ON [dbo].[StudentGroupMemberships]([StudentId] ASC);


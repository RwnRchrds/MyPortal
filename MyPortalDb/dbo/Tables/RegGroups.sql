CREATE TABLE [dbo].[RegGroups] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]        NVARCHAR (128)   NOT NULL,
    [TutorId]     UNIQUEIDENTIFIER NOT NULL,
    [YearGroupId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RegGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RegGroups_StaffMembers_TutorId] FOREIGN KEY ([TutorId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_RegGroups_YearGroups_YearGroupId] FOREIGN KEY ([YearGroupId]) REFERENCES [dbo].[YearGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_RegGroups_TutorId]
    ON [dbo].[RegGroups]([TutorId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RegGroups_YearGroupId]
    ON [dbo].[RegGroups]([YearGroupId] ASC);


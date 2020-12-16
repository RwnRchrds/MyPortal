CREATE TABLE [dbo].[MarksheetTemplateGroups] (
    [Id]                  UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [MarksheetTemplateId] UNIQUEIDENTIFIER NOT NULL,
    [StudentGroupId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_MarksheetTemplateGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MarksheetTemplateGroups_MarksheetTemplates_MarksheetTemplateId] FOREIGN KEY ([MarksheetTemplateId]) REFERENCES [dbo].[MarksheetTemplates] ([Id]),
    CONSTRAINT [FK_MarksheetTemplateGroups_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MarksheetTemplateGroups_MarksheetTemplateId]
    ON [dbo].[MarksheetTemplateGroups]([MarksheetTemplateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MarksheetTemplateGroups_StudentGroupId]
    ON [dbo].[MarksheetTemplateGroups]([StudentGroupId] ASC);


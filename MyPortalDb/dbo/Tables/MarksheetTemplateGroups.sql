CREATE TABLE [dbo].[MarksheetTemplateGroups] (
    [Id]                  UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [MarksheetTemplateId] UNIQUEIDENTIFIER NOT NULL,
    [GroupTypeId]         UNIQUEIDENTIFIER NOT NULL,
    [GroupId]             UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_MarksheetTemplateGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MarksheetTemplateGroups_MarksheetTemplates_MarksheetTemplateId] FOREIGN KEY ([MarksheetTemplateId]) REFERENCES [dbo].[MarksheetTemplates] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MarksheetTemplateGroups_MarksheetTemplateId]
    ON [dbo].[MarksheetTemplateGroups]([MarksheetTemplateId] ASC);


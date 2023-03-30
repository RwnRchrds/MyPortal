CREATE TABLE [dbo].[Marksheets] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]           INT              IDENTITY (1, 1) NOT NULL,
    [MarksheetTemplateId] UNIQUEIDENTIFIER NOT NULL,
    [StudentGroupId]      UNIQUEIDENTIFIER NOT NULL,
    [Completed]           BIT              NOT NULL,
    CONSTRAINT [PK_Marksheets] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Marksheets_MarksheetTemplates_MarksheetTemplateId] FOREIGN KEY ([MarksheetTemplateId]) REFERENCES [dbo].[MarksheetTemplates] ([Id]),
    CONSTRAINT [FK_Marksheets_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [dbo].[StudentGroups] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Marksheets_MarksheetTemplateId]
    ON [dbo].[Marksheets]([MarksheetTemplateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Marksheets_StudentGroupId]
    ON [dbo].[Marksheets]([StudentGroupId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Marksheets]([ClusterId] ASC);


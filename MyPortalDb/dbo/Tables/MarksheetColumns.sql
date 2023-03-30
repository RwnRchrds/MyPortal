CREATE TABLE [dbo].[MarksheetColumns] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [TemplateId]   UNIQUEIDENTIFIER NOT NULL,
    [AspectId]     UNIQUEIDENTIFIER NOT NULL,
    [ResultSetId]  UNIQUEIDENTIFIER NOT NULL,
    [DisplayOrder] INT              NOT NULL,
    [ReadOnly]     BIT              NOT NULL,
    CONSTRAINT [PK_MarksheetColumns] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MarksheetColumns_Aspects_AspectId] FOREIGN KEY ([AspectId]) REFERENCES [dbo].[Aspects] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MarksheetColumns_MarksheetTemplates_TemplateId] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[MarksheetTemplates] ([Id]),
    CONSTRAINT [FK_MarksheetColumns_ResultSets_ResultSetId] FOREIGN KEY ([ResultSetId]) REFERENCES [dbo].[ResultSets] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_MarksheetColumns_AspectId]
    ON [dbo].[MarksheetColumns]([AspectId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MarksheetColumns_ResultSetId]
    ON [dbo].[MarksheetColumns]([ResultSetId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MarksheetColumns_TemplateId]
    ON [dbo].[MarksheetColumns]([TemplateId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[MarksheetColumns]([ClusterId] ASC);


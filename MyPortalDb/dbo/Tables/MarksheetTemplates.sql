CREATE TABLE [dbo].[MarksheetTemplates] (
    [Id]     UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]   NVARCHAR (MAX)   NULL,
    [Active] BIT              NOT NULL,
    CONSTRAINT [PK_MarksheetTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
);


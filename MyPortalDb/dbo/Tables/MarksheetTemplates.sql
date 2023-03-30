CREATE TABLE [dbo].[MarksheetTemplates] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX)   NULL,
    [Notes]     NVARCHAR (MAX)   NULL,
    [Active]    BIT              NOT NULL,
    CONSTRAINT [PK_MarksheetTemplates] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[MarksheetTemplates]([ClusterId] ASC);


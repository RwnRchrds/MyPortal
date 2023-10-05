CREATE TABLE [dbo].[CommentBankSections] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]         INT              IDENTITY (1, 1) NOT NULL,
    [CommentBankAreaId] UNIQUEIDENTIFIER NOT NULL,
    [Name]              NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_CommentBankSections] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommentBankSections_CommentBankAreas_CommentBankAreaId] FOREIGN KEY ([CommentBankAreaId]) REFERENCES [dbo].[CommentBankAreas] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[CommentBankSections]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CommentBankSections_CommentBankAreaId]
    ON [dbo].[CommentBankSections]([CommentBankAreaId] ASC);


CREATE TABLE [dbo].[CommentBankSections] (
    [Id]                UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [CommentBankAreaId] UNIQUEIDENTIFIER NOT NULL,
    [Name]              NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_CommentBankSections] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommentBankSections_CommentBankAreas_CommentBankAreaId] FOREIGN KEY ([CommentBankAreaId]) REFERENCES [dbo].[CommentBankAreas] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CommentBankSections_CommentBankAreaId]
    ON [dbo].[CommentBankSections]([CommentBankAreaId] ASC);


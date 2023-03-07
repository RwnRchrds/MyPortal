CREATE TABLE [dbo].[Comments] (
    [Id]                   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [CommentTypeId]        UNIQUEIDENTIFIER NOT NULL,
    [CommentBankSectionId] UNIQUEIDENTIFIER NOT NULL,
    [Value]                NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comments_CommentBankSections_CommentBankSectionId] FOREIGN KEY ([CommentBankSectionId]) REFERENCES [dbo].[CommentBankSections] ([Id]),
    CONSTRAINT [FK_Comments_CommentTypes_CommentTypeId] FOREIGN KEY ([CommentTypeId]) REFERENCES [dbo].[CommentTypes] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Comments_CommentBankSectionId]
    ON [dbo].[Comments]([CommentBankSectionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Comments_CommentTypeId]
    ON [dbo].[Comments]([CommentTypeId] ASC);


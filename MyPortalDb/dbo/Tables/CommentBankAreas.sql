CREATE TABLE [dbo].[CommentBankAreas] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [CommentBankId] UNIQUEIDENTIFIER NOT NULL,
    [CourseId]      UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (256)   NOT NULL,
    CONSTRAINT [PK_CommentBankAreas] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommentBankAreas_CommentBanks_CommentBankId] FOREIGN KEY ([CommentBankId]) REFERENCES [dbo].[CommentBanks] ([Id]),
    CONSTRAINT [FK_CommentBankAreas_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CommentBankAreas_CommentBankId]
    ON [dbo].[CommentBankAreas]([CommentBankId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CommentBankAreas_CourseId]
    ON [dbo].[CommentBankAreas]([CourseId] ASC);


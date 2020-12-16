CREATE TABLE [dbo].[SenReviews] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]    UNIQUEIDENTIFIER NOT NULL,
    [ReviewTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Date]         DATETIME2 (7)    NOT NULL,
    [Description]  NVARCHAR (256)   NULL,
    [Outcome]      NVARCHAR (256)   NULL,
    CONSTRAINT [PK_SenReviews] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SenReviews_SenReviewTypes_ReviewTypeId] FOREIGN KEY ([ReviewTypeId]) REFERENCES [dbo].[SenReviewTypes] ([Id]),
    CONSTRAINT [FK_SenReviews_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SenReviews_ReviewTypeId]
    ON [dbo].[SenReviews]([ReviewTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenReviews_StudentId]
    ON [dbo].[SenReviews]([StudentId] ASC);


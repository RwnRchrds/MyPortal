CREATE TABLE [dbo].[SenReviewTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_SenReviewTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


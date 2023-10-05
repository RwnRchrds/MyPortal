CREATE TABLE [dbo].[SenReviews] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]          INT              IDENTITY (1, 1) NOT NULL,
    [StudentId]          UNIQUEIDENTIFIER NOT NULL,
    [ReviewTypeId]       UNIQUEIDENTIFIER NOT NULL,
    [ReviewStatusId]     UNIQUEIDENTIFIER NOT NULL,
    [SencoId]            UNIQUEIDENTIFIER NOT NULL,
    [EventId]            UNIQUEIDENTIFIER NOT NULL,
    [OutcomeSenStatusId] UNIQUEIDENTIFIER NULL,
    [Comments]           NVARCHAR (256)   NULL,
    CONSTRAINT [PK_SenReviews] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SenReviews_DiaryEvents_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[DiaryEvents] ([Id]),
    CONSTRAINT [FK_SenReviews_SenReviewStatuses_ReviewStatusId] FOREIGN KEY ([ReviewStatusId]) REFERENCES [dbo].[SenReviewStatuses] ([Id]),
    CONSTRAINT [FK_SenReviews_SenReviewTypes_ReviewTypeId] FOREIGN KEY ([ReviewTypeId]) REFERENCES [dbo].[SenReviewTypes] ([Id]),
    CONSTRAINT [FK_SenReviews_SenStatus_OutcomeSenStatusId] FOREIGN KEY ([OutcomeSenStatusId]) REFERENCES [dbo].[SenStatus] ([Id]),
    CONSTRAINT [FK_SenReviews_StaffMembers_SencoId] FOREIGN KEY ([SencoId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_SenReviews_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[SenReviews]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenReviews_EventId]
    ON [dbo].[SenReviews]([EventId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenReviews_OutcomeSenStatusId]
    ON [dbo].[SenReviews]([OutcomeSenStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenReviews_ReviewStatusId]
    ON [dbo].[SenReviews]([ReviewStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenReviews_ReviewTypeId]
    ON [dbo].[SenReviews]([ReviewTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenReviews_SencoId]
    ON [dbo].[SenReviews]([SencoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenReviews_StudentId]
    ON [dbo].[SenReviews]([StudentId] ASC);


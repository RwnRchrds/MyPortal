CREATE TABLE [dbo].[LessonPlanHomeworkItems] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [LessonPlanId]   UNIQUEIDENTIFIER NOT NULL,
    [HomeworkItemId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_LessonPlanHomeworkItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LessonPlanHomeworkItems_HomeworkItems_HomeworkItemId] FOREIGN KEY ([HomeworkItemId]) REFERENCES [dbo].[HomeworkItems] ([Id]),
    CONSTRAINT [FK_LessonPlanHomeworkItems_LessonPlans_LessonPlanId] FOREIGN KEY ([LessonPlanId]) REFERENCES [dbo].[LessonPlans] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_LessonPlanHomeworkItems_HomeworkItemId]
    ON [dbo].[LessonPlanHomeworkItems]([HomeworkItemId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LessonPlanHomeworkItems_LessonPlanId]
    ON [dbo].[LessonPlanHomeworkItems]([LessonPlanId] ASC);


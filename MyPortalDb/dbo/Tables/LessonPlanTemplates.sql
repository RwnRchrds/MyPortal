CREATE TABLE [dbo].[LessonPlanTemplates] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]         NVARCHAR (256)   NOT NULL,
    [PlanTemplate] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_LessonPlanTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[LessonPlanTemplates] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]    INT              IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (256)   NOT NULL,
    [PlanTemplate] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_LessonPlanTemplates] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[LessonPlanTemplates]([ClusterId] ASC);


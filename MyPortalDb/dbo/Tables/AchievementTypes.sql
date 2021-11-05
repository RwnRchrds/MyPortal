CREATE TABLE [dbo].[AchievementTypes] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [DefaultPoints] INT              NOT NULL,
    [Description]   NVARCHAR (256)   NOT NULL,
    [Active]        BIT              NOT NULL,
    CONSTRAINT [PK_AchievementTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


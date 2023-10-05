CREATE TABLE [dbo].[AchievementTypes] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [Description]   NVARCHAR (256)   NOT NULL,
    [Active]        BIT              NOT NULL,
    [DefaultPoints] INT              NOT NULL,
    CONSTRAINT [PK_AchievementTypes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[AchievementTypes]([ClusterId] ASC);


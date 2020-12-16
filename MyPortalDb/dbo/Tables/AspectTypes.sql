CREATE TABLE [dbo].[AspectTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_AspectTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


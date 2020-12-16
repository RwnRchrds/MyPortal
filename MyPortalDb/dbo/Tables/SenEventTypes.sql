CREATE TABLE [dbo].[SenEventTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_SenEventTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


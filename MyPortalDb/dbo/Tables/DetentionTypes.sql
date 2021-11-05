CREATE TABLE [dbo].[DetentionTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StartTime]   TIME (2)         NOT NULL,
    [EndTime]     TIME (2)         NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_DetentionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


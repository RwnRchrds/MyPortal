CREATE TABLE [dbo].[Activities] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]        NVARCHAR (128)   NULL,
    [Description] NVARCHAR (256)   NULL,
    [DateStarted] DATETIME2 (7)    NOT NULL,
    [DateEnded]   DATETIME2 (7)    NULL,
    [MaxMembers]  INT              NOT NULL,
    [Deleted]     BIT              NOT NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED ([Id] ASC)
);


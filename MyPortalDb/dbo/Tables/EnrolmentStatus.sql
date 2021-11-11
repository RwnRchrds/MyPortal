CREATE TABLE [dbo].[EnrolmentStatus] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Code]        NVARCHAR (MAX)   NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_EnrolmentStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);


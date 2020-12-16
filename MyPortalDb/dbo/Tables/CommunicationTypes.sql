CREATE TABLE [dbo].[CommunicationTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_CommunicationTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


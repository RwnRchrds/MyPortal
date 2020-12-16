CREATE TABLE [dbo].[CommunicationLogs] (
    [Id]                  UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [PersonId]            UNIQUEIDENTIFIER NOT NULL,
    [ContactId]           UNIQUEIDENTIFIER NOT NULL,
    [CommunicationTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Date]                DATETIME2 (7)    NOT NULL,
    [Note]                NVARCHAR (MAX)   NULL,
    [Outgoing]            BIT              NOT NULL,
    CONSTRAINT [PK_CommunicationLogs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommunicationLogs_CommunicationTypes_CommunicationTypeId] FOREIGN KEY ([CommunicationTypeId]) REFERENCES [dbo].[CommunicationTypes] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CommunicationLogs_CommunicationTypeId]
    ON [dbo].[CommunicationLogs]([CommunicationTypeId] ASC);


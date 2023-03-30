CREATE TABLE [dbo].[CommunicationLogs] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]           INT              IDENTITY (1, 1) NOT NULL,
    [ContactId]           UNIQUEIDENTIFIER NOT NULL,
    [CommunicationTypeId] UNIQUEIDENTIFIER NOT NULL,
    [Date]                DATETIME2 (7)    NOT NULL,
    [Notes]               NVARCHAR (MAX)   NULL,
    [Outgoing]            BIT              NOT NULL,
    CONSTRAINT [PK_CommunicationLogs] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommunicationLogs_CommunicationTypes_CommunicationTypeId] FOREIGN KEY ([CommunicationTypeId]) REFERENCES [dbo].[CommunicationTypes] ([Id]),
    CONSTRAINT [FK_CommunicationLogs_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_CommunicationLogs_CommunicationTypeId]
    ON [dbo].[CommunicationLogs]([CommunicationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CommunicationLogs_ContactId]
    ON [dbo].[CommunicationLogs]([ContactId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[CommunicationLogs]([ClusterId] ASC);


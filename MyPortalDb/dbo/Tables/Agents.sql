CREATE TABLE [dbo].[Agents] (
    [Id]       UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [AgencyId] UNIQUEIDENTIFIER NOT NULL,
    [PersonId] UNIQUEIDENTIFIER NOT NULL,
    [JobTitle] NVARCHAR (128)   NULL,
    [Deleted]  BIT              NOT NULL,
    CONSTRAINT [PK_Agents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Agents_Agencies_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agencies] ([Id]),
    CONSTRAINT [FK_Agents_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Agents_AgencyId]
    ON [dbo].[Agents]([AgencyId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Agents_PersonId]
    ON [dbo].[Agents]([PersonId] ASC);


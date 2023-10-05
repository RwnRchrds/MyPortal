CREATE TABLE [dbo].[Schools] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]           INT              IDENTITY (1, 1) NOT NULL,
    [AgencyId]            UNIQUEIDENTIFIER NOT NULL,
    [LocalAuthorityId]    UNIQUEIDENTIFIER NOT NULL,
    [EstablishmentNumber] INT              NOT NULL,
    [Urn]                 NVARCHAR (128)   NOT NULL,
    [Uprn]                NVARCHAR (128)   NOT NULL,
    [PhaseId]             UNIQUEIDENTIFIER NOT NULL,
    [TypeId]              UNIQUEIDENTIFIER NOT NULL,
    [GovernanceTypeId]    UNIQUEIDENTIFIER NOT NULL,
    [IntakeTypeId]        UNIQUEIDENTIFIER NOT NULL,
    [HeadTeacherId]       UNIQUEIDENTIFIER NULL,
    [Local]               BIT              NOT NULL,
    CONSTRAINT [PK_Schools] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Schools_Agencies_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agencies] ([Id]),
    CONSTRAINT [FK_Schools_GovernanceTypes_GovernanceTypeId] FOREIGN KEY ([GovernanceTypeId]) REFERENCES [dbo].[GovernanceTypes] ([Id]),
    CONSTRAINT [FK_Schools_IntakeTypes_IntakeTypeId] FOREIGN KEY ([IntakeTypeId]) REFERENCES [dbo].[IntakeTypes] ([Id]),
    CONSTRAINT [FK_Schools_LocalAuthorities_LocalAuthorityId] FOREIGN KEY ([LocalAuthorityId]) REFERENCES [dbo].[LocalAuthorities] ([Id]),
    CONSTRAINT [FK_Schools_People_HeadTeacherId] FOREIGN KEY ([HeadTeacherId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_Schools_SchoolPhases_PhaseId] FOREIGN KEY ([PhaseId]) REFERENCES [dbo].[SchoolPhases] ([Id]),
    CONSTRAINT [FK_Schools_SchoolTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[SchoolTypes] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Schools]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Schools_AgencyId]
    ON [dbo].[Schools]([AgencyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Schools_GovernanceTypeId]
    ON [dbo].[Schools]([GovernanceTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Schools_HeadTeacherId]
    ON [dbo].[Schools]([HeadTeacherId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Schools_IntakeTypeId]
    ON [dbo].[Schools]([IntakeTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Schools_LocalAuthorityId]
    ON [dbo].[Schools]([LocalAuthorityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Schools_PhaseId]
    ON [dbo].[Schools]([PhaseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Schools_TypeId]
    ON [dbo].[Schools]([TypeId] ASC);


CREATE TABLE [dbo].[PersonDietaryRequirements] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]            INT              IDENTITY (1, 1) NOT NULL,
    [PersonId]             UNIQUEIDENTIFIER NOT NULL,
    [DietaryRequirementId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_PersonDietaryRequirements] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonDietaryRequirements_DietaryRequirements_DietaryRequirementId] FOREIGN KEY ([DietaryRequirementId]) REFERENCES [dbo].[DietaryRequirements] ([Id]),
    CONSTRAINT [FK_PersonDietaryRequirements_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[PersonDietaryRequirements]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PersonDietaryRequirements_DietaryRequirementId]
    ON [dbo].[PersonDietaryRequirements]([DietaryRequirementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PersonDietaryRequirements_PersonId]
    ON [dbo].[PersonDietaryRequirements]([PersonId] ASC);


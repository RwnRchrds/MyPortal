CREATE TABLE [dbo].[PersonConditions] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]       INT              IDENTITY (1, 1) NOT NULL,
    [PersonId]        UNIQUEIDENTIFIER NOT NULL,
    [ConditionId]     UNIQUEIDENTIFIER NOT NULL,
    [MedicationTaken] BIT              NOT NULL,
    [Medication]      NVARCHAR (256)   NULL,
    CONSTRAINT [PK_PersonConditions] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonConditions_MedicalConditions_ConditionId] FOREIGN KEY ([ConditionId]) REFERENCES [dbo].[MedicalConditions] ([Id]),
    CONSTRAINT [FK_PersonConditions_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_PersonConditions_ConditionId]
    ON [dbo].[PersonConditions]([ConditionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PersonConditions_PersonId]
    ON [dbo].[PersonConditions]([PersonId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[PersonConditions]([ClusterId] ASC);


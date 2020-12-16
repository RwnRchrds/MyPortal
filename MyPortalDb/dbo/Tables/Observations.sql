CREATE TABLE [dbo].[Observations] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Date]       DATE             NOT NULL,
    [ObserveeId] UNIQUEIDENTIFIER NOT NULL,
    [ObserverId] UNIQUEIDENTIFIER NOT NULL,
    [OutcomeId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Observations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Observations_ObservationOutcomes_OutcomeId] FOREIGN KEY ([OutcomeId]) REFERENCES [dbo].[ObservationOutcomes] ([Id]),
    CONSTRAINT [FK_Observations_StaffMembers_ObserveeId] FOREIGN KEY ([ObserveeId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_Observations_StaffMembers_ObserverId] FOREIGN KEY ([ObserverId]) REFERENCES [dbo].[StaffMembers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Observations_ObserveeId]
    ON [dbo].[Observations]([ObserveeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Observations_ObserverId]
    ON [dbo].[Observations]([ObserverId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Observations_OutcomeId]
    ON [dbo].[Observations]([OutcomeId] ASC);


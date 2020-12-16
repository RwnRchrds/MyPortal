CREATE TABLE [dbo].[Students] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [PersonId]        UNIQUEIDENTIFIER NOT NULL,
    [RegGroupId]      UNIQUEIDENTIFIER NOT NULL,
    [YearGroupId]     UNIQUEIDENTIFIER NOT NULL,
    [HouseId]         UNIQUEIDENTIFIER NULL,
    [AdmissionNumber] INT              NOT NULL,
    [DateStarting]    DATE             NULL,
    [DateLeaving]     DATE             NULL,
    [FreeSchoolMeals] BIT              NOT NULL,
    [SenStatusId]     UNIQUEIDENTIFIER NULL,
    [PupilPremium]    BIT              NOT NULL,
    [Upn]             VARCHAR (13)     NULL,
    [Deleted]         BIT              NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Students_Houses_HouseId] FOREIGN KEY ([HouseId]) REFERENCES [dbo].[Houses] ([Id]),
    CONSTRAINT [FK_Students_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_Students_RegGroups_RegGroupId] FOREIGN KEY ([RegGroupId]) REFERENCES [dbo].[RegGroups] ([Id]),
    CONSTRAINT [FK_Students_SenStatus_SenStatusId] FOREIGN KEY ([SenStatusId]) REFERENCES [dbo].[SenStatus] ([Id]),
    CONSTRAINT [FK_Students_YearGroups_YearGroupId] FOREIGN KEY ([YearGroupId]) REFERENCES [dbo].[YearGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Students_HouseId]
    ON [dbo].[Students]([HouseId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Students_PersonId]
    ON [dbo].[Students]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Students_RegGroupId]
    ON [dbo].[Students]([RegGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Students_SenStatusId]
    ON [dbo].[Students]([SenStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Students_YearGroupId]
    ON [dbo].[Students]([YearGroupId] ASC);


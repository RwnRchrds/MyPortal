CREATE TABLE [dbo].[Students] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]         INT              IDENTITY (1, 1) NOT NULL,
    [PersonId]          UNIQUEIDENTIFIER NOT NULL,
    [AdmissionNumber]   INT              NOT NULL,
    [DateStarting]      DATE             NULL,
    [DateLeaving]       DATE             NULL,
    [FreeSchoolMeals]   BIT              NOT NULL,
    [SenStatusId]       UNIQUEIDENTIFIER NULL,
    [SenTypeId]         UNIQUEIDENTIFIER NULL,
    [EnrolmentStatusId] UNIQUEIDENTIFIER NULL,
    [BoarderStatusId]   UNIQUEIDENTIFIER NULL,
    [PupilPremium]      BIT              NOT NULL,
    [Upn]               VARCHAR (13)     NULL,
    [Deleted]           BIT              NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Students_BoarderStatus_BoarderStatusId] FOREIGN KEY ([BoarderStatusId]) REFERENCES [dbo].[BoarderStatus] ([Id]),
    CONSTRAINT [FK_Students_EnrolmentStatus_EnrolmentStatusId] FOREIGN KEY ([EnrolmentStatusId]) REFERENCES [dbo].[EnrolmentStatus] ([Id]),
    CONSTRAINT [FK_Students_People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id]),
    CONSTRAINT [FK_Students_SenStatus_SenStatusId] FOREIGN KEY ([SenStatusId]) REFERENCES [dbo].[SenStatus] ([Id]),
    CONSTRAINT [FK_Students_SenTypes_SenTypeId] FOREIGN KEY ([SenTypeId]) REFERENCES [dbo].[SenTypes] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Students]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Students_BoarderStatusId]
    ON [dbo].[Students]([BoarderStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Students_EnrolmentStatusId]
    ON [dbo].[Students]([EnrolmentStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Students_PersonId]
    ON [dbo].[Students]([PersonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Students_SenStatusId]
    ON [dbo].[Students]([SenStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Students_SenTypeId]
    ON [dbo].[Students]([SenTypeId] ASC);


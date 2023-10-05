CREATE TABLE [dbo].[TrainingCertificates] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [CourseId]  UNIQUEIDENTIFIER NOT NULL,
    [StaffId]   UNIQUEIDENTIFIER NOT NULL,
    [StatusId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TrainingCertificates] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TrainingCertificates_StaffMembers_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[StaffMembers] ([Id]),
    CONSTRAINT [FK_TrainingCertificates_TrainingCertificateStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[TrainingCertificateStatus] ([Id]),
    CONSTRAINT [FK_TrainingCertificates_TrainingCourses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[TrainingCourses] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[TrainingCertificates]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TrainingCertificates_CourseId]
    ON [dbo].[TrainingCertificates]([CourseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TrainingCertificates_StaffId]
    ON [dbo].[TrainingCertificates]([StaffId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TrainingCertificates_StatusId]
    ON [dbo].[TrainingCertificates]([StatusId] ASC);


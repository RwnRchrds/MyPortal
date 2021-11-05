CREATE TABLE [dbo].[MedicalEvents] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]   UNIQUEIDENTIFIER NOT NULL,
    [CreatedById] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate] DATETIME2 (7)    NOT NULL,
    [Date]        DATE             NOT NULL,
    [Note]        NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_MedicalEvents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalEvents_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_MedicalEvents_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalEvents_CreatedById]
    ON [dbo].[MedicalEvents]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalEvents_StudentId]
    ON [dbo].[MedicalEvents]([StudentId] ASC);


﻿CREATE TABLE [dbo].[LogNotes] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]      INT              IDENTITY (1, 1) NOT NULL,
    [TypeId]         UNIQUEIDENTIFIER NOT NULL,
    [CreatedById]    UNIQUEIDENTIFIER NOT NULL,
    [StudentId]      UNIQUEIDENTIFIER NOT NULL,
    [AcademicYearId] UNIQUEIDENTIFIER NOT NULL,
    [Message]        NVARCHAR (MAX)   NOT NULL,
    [CreatedDate]    DATETIME2 (7)    NOT NULL,
    [Private]        BIT              NOT NULL,
    [Deleted]        BIT              NOT NULL,
    CONSTRAINT [PK_LogNotes] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LogNotes_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYears] ([Id]),
    CONSTRAINT [FK_LogNotes_LogNoteTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[LogNoteTypes] ([Id]),
    CONSTRAINT [FK_LogNotes_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_LogNotes_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[LogNotes]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LogNotes_AcademicYearId]
    ON [dbo].[LogNotes]([AcademicYearId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LogNotes_CreatedById]
    ON [dbo].[LogNotes]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LogNotes_StudentId]
    ON [dbo].[LogNotes]([StudentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LogNotes_TypeId]
    ON [dbo].[LogNotes]([TypeId] ASC);


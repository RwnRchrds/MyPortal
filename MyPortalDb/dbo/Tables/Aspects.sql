﻿CREATE TABLE [dbo].[Aspects] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]     INT              IDENTITY (1, 1) NOT NULL,
    [Description]   NVARCHAR (256)   NOT NULL,
    [Active]        BIT              NOT NULL,
    [TypeId]        UNIQUEIDENTIFIER NOT NULL,
    [GradeSetId]    UNIQUEIDENTIFIER NULL,
    [MinMark]       DECIMAL (10, 2)  NULL,
    [MaxMark]       DECIMAL (10, 2)  NULL,
    [Name]          NVARCHAR (128)   NOT NULL,
    [ColumnHeading] NVARCHAR (50)    NOT NULL,
    [Private]       BIT              NOT NULL,
    [System]        BIT              NOT NULL,
    CONSTRAINT [PK_Aspects] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Aspects_AspectTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[AspectTypes] ([Id]),
    CONSTRAINT [FK_Aspects_GradeSets_GradeSetId] FOREIGN KEY ([GradeSetId]) REFERENCES [dbo].[GradeSets] ([Id])
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[Aspects]([ClusterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Aspects_GradeSetId]
    ON [dbo].[Aspects]([GradeSetId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Aspects_TypeId]
    ON [dbo].[Aspects]([TypeId] ASC);


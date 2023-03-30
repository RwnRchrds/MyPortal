CREATE TABLE [dbo].[GiftedTalentedStudents] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [ClusterId] INT              IDENTITY (1, 1) NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [SubjectId] UNIQUEIDENTIFIER NOT NULL,
    [Notes]     NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_GiftedTalentedStudents] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GiftedTalentedStudents_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_GiftedTalentedStudents_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_GiftedTalentedStudents_StudentId]
    ON [dbo].[GiftedTalentedStudents]([StudentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GiftedTalentedStudents_SubjectId]
    ON [dbo].[GiftedTalentedStudents]([SubjectId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[GiftedTalentedStudents]([ClusterId] ASC);


CREATE TABLE [dbo].[TrainingCourses] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ClusterId]   INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Code]        NVARCHAR (128)   NOT NULL,
    [Name]        NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_TrainingCourses] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE CLUSTERED INDEX [CIX_ClusterId]
    ON [dbo].[TrainingCourses]([ClusterId] ASC);


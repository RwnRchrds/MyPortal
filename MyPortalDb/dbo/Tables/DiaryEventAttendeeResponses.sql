CREATE TABLE [dbo].[DiaryEventAttendeeResponses] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    CONSTRAINT [PK_DiaryEventAttendeeResponses] PRIMARY KEY CLUSTERED ([Id] ASC)
);


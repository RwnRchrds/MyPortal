CREATE TABLE [dbo].[StaffAbsenceTypes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (256)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [System]      BIT              NOT NULL,
    [Illness]     BIT              NOT NULL,
    CONSTRAINT [PK_StaffAbsenceTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


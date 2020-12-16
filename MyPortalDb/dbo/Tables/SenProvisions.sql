CREATE TABLE [dbo].[SenProvisions] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]       UNIQUEIDENTIFIER NOT NULL,
    [ProvisionTypeId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate]       DATE             NOT NULL,
    [EndDate]         DATE             NOT NULL,
    [Note]            NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_SenProvisions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SenProvisions_SenProvisionTypes_ProvisionTypeId] FOREIGN KEY ([ProvisionTypeId]) REFERENCES [dbo].[SenProvisionTypes] ([Id]),
    CONSTRAINT [FK_SenProvisions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SenProvisions_ProvisionTypeId]
    ON [dbo].[SenProvisions]([ProvisionTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SenProvisions_StudentId]
    ON [dbo].[SenProvisions]([StudentId] ASC);


CREATE TABLE [dbo].[StudentCharges] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [StudentId]   UNIQUEIDENTIFIER NOT NULL,
    [ChargeId]    UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate] DATETIME2 (7)    NOT NULL,
    [Recurrences] INT              NOT NULL,
    CONSTRAINT [PK_StudentCharges] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentCharges_Charges_ChargeId] FOREIGN KEY ([ChargeId]) REFERENCES [dbo].[Charges] ([Id]),
    CONSTRAINT [FK_StudentCharges_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentCharges_ChargeId]
    ON [dbo].[StudentCharges]([ChargeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentCharges_StudentId]
    ON [dbo].[StudentCharges]([StudentId] ASC);


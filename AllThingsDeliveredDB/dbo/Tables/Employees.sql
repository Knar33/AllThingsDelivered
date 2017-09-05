CREATE TABLE [dbo].[Employees] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [FName]        NVARCHAR (100) NULL,
    [LName]        NVARCHAR (100) NULL,
    [DOB]          DATE           NOT NULL,
    [SupervisorID] INT            NULL,
    [AddressID]    INT            NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Employees_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([ID]) ON DELETE SET NULL,
    CONSTRAINT [FK_Employees_Supervisor] FOREIGN KEY ([SupervisorID]) REFERENCES [dbo].[Employees] ([ID])
);


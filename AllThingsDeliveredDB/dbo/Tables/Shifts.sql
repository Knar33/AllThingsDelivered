CREATE TABLE [dbo].[Shifts] (
    [ID]            INT        IDENTITY (1, 1) NOT NULL,
    [StoreID]       INT        NOT NULL,
    [EmployeeID]    INT        NOT NULL,
    [StartDateTime] DATETIME   NOT NULL,
    [EndDateTime]   DATETIME   NOT NULL,
    [Wage]          SMALLMONEY NOT NULL,
    CONSTRAINT [PK_Shifts] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Shifts_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Shifts_Stores] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Stores] ([ID]) ON DELETE CASCADE
);


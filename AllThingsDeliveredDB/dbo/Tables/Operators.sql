CREATE TABLE [dbo].[Operators] (
    [OperatorID] INT            IDENTITY (1, 1) NOT NULL,
    [AddressID]  INT            NOT NULL,
    [FirstName]  NVARCHAR (100) NOT NULL,
    [LastName]   NVARCHAR (100) NOT NULL,
    [StartDate]  DATE           NOT NULL,
    [EndDate]    DATE           NOT NULL,
    CONSTRAINT [PK_Operators] PRIMARY KEY CLUSTERED ([OperatorID] ASC),
    CONSTRAINT [FK_Operators_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressID])
);


CREATE TABLE [dbo].[Customers] (
    [CustomerID]  INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (100) NOT NULL,
    [LastName]    NVARCHAR (100) NOT NULL,
    [PhoneNumber] NVARCHAR (100) NOT NULL,
    [Email]       NVARCHAR (256) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);


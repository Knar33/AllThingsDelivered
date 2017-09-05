CREATE TABLE [dbo].[CustomerAddresses] (
    [CustomerID] INT NOT NULL,
    [AddressID]  INT NOT NULL,
    CONSTRAINT [PK_CustomerAddresses] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [AddressID] ASC),
    CONSTRAINT [FK_CustomerAddresses_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerAddresses_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]) ON DELETE CASCADE
);


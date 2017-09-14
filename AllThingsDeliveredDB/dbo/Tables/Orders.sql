CREATE TABLE [dbo].[Orders] (
    [OrderID]      INT            IDENTITY (1, 1) NOT NULL,
    [AddressID]    INT            NOT NULL,
    [DriverID]     INT            NULL,
    [CustomerID]   INT            NOT NULL,
    [OperatorID]   INT            NULL,
    [TimePlaced]   DATETIME       NOT NULL,
    [OrderMethod]  NVARCHAR (100) NOT NULL,
    [Active]       BIT            NOT NULL,
    [Updating]     BIT            NOT NULL,
    [Updated]      BIT            NOT NULL,
    [OrderPrice]   DECIMAL (18)   NULL,
    [DeliveryFee]  DECIMAL (18)   NULL,
    [Customertip]  DECIMAL (18)   NULL,
    [DriverRating] INT            NULL,
    [Customize] NVARCHAR(1000) NULL, 
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC),
    CONSTRAINT [FK_Orders_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressID]),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID]),
    CONSTRAINT [FK_Orders_Drivers] FOREIGN KEY ([DriverID]) REFERENCES [dbo].[Drivers] ([DriverID]),
    CONSTRAINT [FK_Orders_Operators] FOREIGN KEY ([OperatorID]) REFERENCES [dbo].[Operators] ([OperatorID])
);


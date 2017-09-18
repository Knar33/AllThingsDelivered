CREATE TABLE [dbo].[Orders] (
    [OrderID]      INT            IDENTITY (1, 1) NOT NULL,
    [AddressID]    INT            NOT NULL,
    [DriverID]     INT            NULL,
    [CustomerID]   INT            NOT NULL,
    [TimePlaced]   DATETIME       NOT NULL,
    [Active]       BIT            NOT NULL,
    [OrderPrice]   DECIMAL (18, 2)   NULL,
    [DeliveryFee]  DECIMAL (18, 2)   NULL,
    [Customertip]  DECIMAL (18, 2)   NULL,
    [DriverRating] INT            NULL,
    [Customize] NVARCHAR(1000) NULL, 
    [PaymentToken] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC),
    CONSTRAINT [FK_Orders_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressID]),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID]),
    CONSTRAINT [FK_Orders_Drivers] FOREIGN KEY ([DriverID]) REFERENCES [dbo].[Drivers] ([DriverID])
);


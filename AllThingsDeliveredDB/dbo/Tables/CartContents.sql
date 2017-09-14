CREATE TABLE [dbo].[CartContents] (
    [CartContentID]   INT             IDENTITY (1, 1) NOT NULL,
    [CustomerID]      INT             NOT NULL,
    [RestaurantID]    INT             NOT NULL,
    [ItemName]        NVARCHAR (1000) NOT NULL,
    [ItemDescription] NVARCHAR (2000) NULL,
    [Quantity]        INT             NOT NULL,
    [Price]           DECIMAL (18, 2)    NULL,
    [Customize]       NVARCHAR (1000) NULL,
    CONSTRAINT [PK_CartContents] PRIMARY KEY CLUSTERED ([CartContentID] ASC),
    CONSTRAINT [FK_CartContents_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);


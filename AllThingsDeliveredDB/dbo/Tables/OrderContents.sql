CREATE TABLE [dbo].[OrderContents] (
    [OrderContentID]  INT             IDENTITY (1, 1) NOT NULL,
    [OrderID]         INT             NOT NULL,
    [RestaurantID]    INT             NOT NULL,
    [ItemName]        NVARCHAR (1000) NOT NULL,
    [ItemDescription] NVARCHAR (2000) NULL,
    [Quantity]        INT             NOT NULL,
    [Price]           DECIMAL (18)    NULL,
    [Customize]       NVARCHAR (1000) NULL,
    CONSTRAINT [PK_OrderContents] PRIMARY KEY CLUSTERED ([OrderContentID] ASC),
    CONSTRAINT [FK_OrderContents_Orders] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID])
);


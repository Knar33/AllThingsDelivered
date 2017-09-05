CREATE TABLE [dbo].[Products] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [UPC]          NVARCHAR (50)  NULL,
    [Price]        MONEY          DEFAULT ((0)) NOT NULL,
    [Manufacturer] INT            NULL,
    [ProductType]  INT            NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Products_Manufacturer] FOREIGN KEY ([Manufacturer]) REFERENCES [dbo].[Manufacturers] ([ID]) ON DELETE SET NULL,
    CONSTRAINT [FK_Products_ProductType] FOREIGN KEY ([ProductType]) REFERENCES [dbo].[ProductTypes] ([ID]) ON DELETE SET NULL
);


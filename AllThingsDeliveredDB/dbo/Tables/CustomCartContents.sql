CREATE TABLE [dbo].[CustomCartContents] (
    [CustomCartContentsID] INT             IDENTITY (1, 1) NOT NULL,
    [ItemLocation]         NVARCHAR (1000) NULL,
    [Content]              NVARCHAR (1000) NOT NULL,
    [CustomerID]           INT             NOT NULL,
    CONSTRAINT [PK_CustomCartContents] PRIMARY KEY CLUSTERED ([CustomCartContentsID] ASC),
    CONSTRAINT [FK_CustomCartContents_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);


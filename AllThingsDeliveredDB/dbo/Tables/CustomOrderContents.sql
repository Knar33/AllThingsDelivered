CREATE TABLE [dbo].[CustomOrderContents] (
    [CustomOrderContentsID] INT             IDENTITY (1, 1) NOT NULL,
    [ItemLocation]          NVARCHAR (1000) NULL,
    [Content]               NVARCHAR (1000) NOT NULL,
    [OrderID]               INT             NOT NULL,
    CONSTRAINT [PK_CustomOrderContents] PRIMARY KEY CLUSTERED ([CustomOrderContentsID] ASC),
    CONSTRAINT [FK_CustomOrderContents_Orders] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID])
);


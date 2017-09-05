CREATE TABLE [dbo].[ProductTypes] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [TypeName] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
);


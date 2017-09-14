﻿CREATE TABLE [dbo].[Countries]
(
	[CountryID] INT IDENTITY(1,1) NOT NULL,
	[CountryName] NVARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryID] ASC)
)

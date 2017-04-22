CREATE TABLE [dbo].[tbl_Master_Country] (
    [CountryId]   INT          IDENTITY (1, 1) NOT NULL,
    [CountryName] VARCHAR (50) NOT NULL,
    [CountryCode] VARCHAR (50) NOT NULL,
    [Enabled]     BIT          NULL
);


CREATE TABLE [dbo].[tbl_QuickListing] (
    [ListingID]      UNIQUEIDENTIFIER CONSTRAINT [DF_tbl_QuickListing_ListingID] DEFAULT (newid()) NULL,
    [RegistrationID] UNIQUEIDENTIFIER NULL,
    [ContactFor]     VARCHAR (50)     NOT NULL,
    [Name]           VARCHAR (50)     NOT NULL,
    [Email]          VARCHAR (50)     NOT NULL,
    [Mobile]         VARCHAR (20)     NOT NULL,
    [CreateDate]     DATE             CONSTRAINT [DF_tbl_QuickListing_CreateDate] DEFAULT (getdate()) NOT NULL,
    [CreateBy]       VARCHAR (50)     NULL,
    [Enabled]        BIT              NOT NULL
);


CREATE TABLE [dbo].[tbl_ContactUs] (
    [ContactUsID]    UNIQUEIDENTIFIER CONSTRAINT [DF_tbl_ContactUs_ContactUsID] DEFAULT (newid()) NULL,
    [RegistrationID] UNIQUEIDENTIFIER NULL,
    [Name]           VARCHAR (50)     NOT NULL,
    [Mobile]         VARCHAR (20)     NULL,
    [Email]          VARCHAR (50)     NULL,
    [QueryFrom]      VARCHAR (50)     NULL,
    [QueryType]      VARCHAR (50)     NULL,
    [Query]          VARCHAR (MAX)    NULL,
    [CreateDate]     DATE             CONSTRAINT [DF_tbl_ContactUs_CreateDate] DEFAULT (getdate()) NOT NULL,
    [CreatedBy]      NUMERIC (18)     NULL,
    [Enabled]        BIT              NOT NULL
);


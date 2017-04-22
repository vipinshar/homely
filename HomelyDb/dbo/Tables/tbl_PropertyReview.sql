CREATE TABLE [dbo].[tbl_PropertyReview] (
    [ReviewID]        UNIQUEIDENTIFIER CONSTRAINT [DF_tbl_PropertyReview_ReviewID] DEFAULT (newid()) NOT NULL,
    [RegistrationID]  UNIQUEIDENTIFIER NULL,
    [Name]            VARCHAR (50)     NULL,
    [Mobile]          VARCHAR (15)     NULL,
    [EmailId]         VARCHAR (50)     NULL,
    [Review]          NVARCHAR (MAX)   NULL,
    [HomePage]        BIT              CONSTRAINT [DF_tbl_PropertyReview_HomePage] DEFAULT ((0)) NULL,
    [CreateDate]      DATETIME         CONSTRAINT [DF_tbl_PropertyReview_CreateDate] DEFAULT (getdate()) NOT NULL,
    [Enabled]         BIT              CONSTRAINT [DF_tbl_PropertyReview_Enabled] DEFAULT ((1)) NOT NULL,
    [OwnershipType]   NUMERIC (18)     NULL,
    [ReviewFor]       VARCHAR (50)     NULL,
    [ReviewForValue]  VARCHAR (50)     NULL,
    [RatingLocation1] VARCHAR (10)     NULL,
    [RatingLocation2] VARCHAR (10)     NULL,
    [RatingLocation3] VARCHAR (10)     NULL,
    [RatingLocation4] VARCHAR (10)     NULL,
    [RatingLocation5] VARCHAR (10)     NULL,
    [RatingLocation6] VARCHAR (10)     NULL,
    [RatingSociety1]  VARCHAR (10)     NULL,
    [RatingSociety2]  VARCHAR (10)     NULL,
    [RatingSociety3]  VARCHAR (10)     NULL,
    [RatingSociety4]  VARCHAR (10)     NULL,
    [RatingSociety5]  VARCHAR (10)     NULL,
    [RatingSociety6]  VARCHAR (10)     NULL,
    [RatingSociety7]  VARCHAR (10)     NULL,
    [Approved]        BIT              CONSTRAINT [DF_tbl_PropertyReview_Approved] DEFAULT ((0)) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'QUALITY OF LIFE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingLocation1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GROWTH ORIENTED CITY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingLocation2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ACCLAIMED SCHOOLS AND EDUCATIONAL HUBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingLocation3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ACCLAIMED HOSPITAL AND CARE SERVICES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingLocation4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EASE OF TRANSPORTATION IN CITY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingLocation5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'LIFESTYLE FRIENDLY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingLocation6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EASY ACCESS TO DAILY LIFESTYLE NEEDS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingSociety1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SOCIETY MAINTENANCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingSociety2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SELF SUFFICIENT SOCIETY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingSociety3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EFFECTIVE SOCIETY COMMITTEE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingSociety4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SAFETY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingSociety5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FAMILY ORIENTED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingSociety6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FRIENDLY NEIGHBORS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_PropertyReview', @level2type = N'COLUMN', @level2name = N'RatingSociety7';


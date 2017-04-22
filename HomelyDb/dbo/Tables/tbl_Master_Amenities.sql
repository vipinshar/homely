CREATE TABLE [dbo].[tbl_Master_Amenities] (
    [AmenityId]         NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [Amenity]           VARCHAR (20) NOT NULL,
    [ImagePath]         VARCHAR (50) NULL,
    [BigImagePath]      VARCHAR (50) NULL,
    [BigImageHoverPath] VARCHAR (50) NULL,
    [Enabled]           BIT          CONSTRAINT [DF_tbl_Master_Amenities_Enabled] DEFAULT ((1)) NOT NULL,
    [Order]             NUMERIC (18) CONSTRAINT [DF_tbl_Master_Amenities_Order] DEFAULT ((0)) NOT NULL,
    [CreateDate]        DATETIME     CONSTRAINT [DF_tbl_Master_Amenities_CreateDate] DEFAULT (getdate()) NOT NULL,
    [ImageHoverPath]    VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_Master_Amenities] PRIMARY KEY CLUSTERED ([Amenity] ASC)
);

